using System.Text.RegularExpressions;
using ProjetoMei.Back.Models;
using ProjetoMei.Back.Repository;

namespace ProjetoMei.Back.Services
{
    public class UsuarioMeiService : IUsuarioMeiService
    {
        private readonly IUsuarioMeiRepository _usuarioMeiRepository;
        private readonly IAdministradorRepository _administradorRepository;

        public UsuarioMeiService(IUsuarioMeiRepository usuarioMeiRepository, IAdministradorRepository administradorRepository)
        {
            _usuarioMeiRepository = usuarioMeiRepository;
            _administradorRepository = administradorRepository;
        }

        public UsuarioMeiModel Adicionar(UsuarioMeiDataModel usuarioMeiDataModel)
        {
            ArgumentNullException.ThrowIfNull(usuarioMeiDataModel);

            Normalizar(usuarioMeiDataModel);
            ValidarSenha(usuarioMeiDataModel.Senha);

            if (_administradorRepository.ExisteEmail(usuarioMeiDataModel.Email)
                || _usuarioMeiRepository.ExisteCpfOuEmail(usuarioMeiDataModel.Cpf, usuarioMeiDataModel.Email))
            {
                throw new InvalidOperationException("Já existe um cadastro com este CPF ou e-mail.");
            }

            return _usuarioMeiRepository.Adicionar(usuarioMeiDataModel, SenhaHasher.Gerar(usuarioMeiDataModel.Senha));
        }

        public List<UsuarioMeiModel> BuscarUsuarios()
        {
            return _usuarioMeiRepository.BuscarUsuarios();
        }

        public UsuarioMeiModel BuscarUsuarioPorId(int Id)
        {
            return _usuarioMeiRepository.BuscarUsuarioPorId(Id);
        }

        public UsuarioMeiModel BuscarUsuarioPorNome(string Nome)
        {
            return _usuarioMeiRepository.BuscarUsuarioPorNome(Nome.Trim());
        }

        public UsuarioMeiModel Atualizar(UsuarioMeiDataModel usuarioMeiDataModel, int Id)
        {
            ArgumentNullException.ThrowIfNull(usuarioMeiDataModel);

            Normalizar(usuarioMeiDataModel);

            if (_usuarioMeiRepository.ExisteCpfOuEmail(usuarioMeiDataModel.Cpf, usuarioMeiDataModel.Email, Id))
            {
                throw new InvalidOperationException("Já existe um cadastro com este CPF ou e-mail.");
            }

            // Senha vazia = manter a atual; preenchida = trocar.
            string? novoHash = null;
            if (!string.IsNullOrEmpty(usuarioMeiDataModel.Senha))
            {
                ValidarSenha(usuarioMeiDataModel.Senha);
                novoHash = SenhaHasher.Gerar(usuarioMeiDataModel.Senha);
            }

            return _usuarioMeiRepository.Atualizar(usuarioMeiDataModel, Id, novoHash);
        }

        public bool Deletar(int Id)
        {
            return _usuarioMeiRepository.Deletar(Id);
        }

        public List<UsuarioMeiVitrineModel> BuscarVitrine()
        {
            return _usuarioMeiRepository.BuscarVitrine()
                .Select(u => new UsuarioMeiVitrineModel
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Cidade = u.Cidade,
                    Estado = u.Estado,
                    RamoAtividade = u.RamoAtividade,
                    Descricao = u.Descricao,
                    Experiencia = u.Experiencia,
                    Portfolio = u.Portfolio,
                    Categorias = u.Categorias
                        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                        .ToList(),
                    Plano = u.Plano,
                    DataCadastro = u.DataCadastro
                })
                .ToList();
        }

        public UsuarioMeiLoginModel? Autenticar(LoginDataModel login)
        {
            ArgumentNullException.ThrowIfNull(login);

            string email = login.Email.Trim().ToLowerInvariant();

            // 1) Administrador?
            AdministradorModel? admin = _administradorRepository.BuscarPorEmail(email);
            if (admin != null)
            {
                if (!SenhaHasher.Verificar(login.Senha, admin.SenhaHash))
                {
                    return null;
                }

                return new UsuarioMeiLoginModel
                {
                    Id = admin.Id,
                    Nome = admin.Nome,
                    Email = admin.Email,
                    Plano = "admin",
                    Perfil = "admin"
                };
            }

            // 2) MEI. Mesma resposta (null = 401) para "e-mail não existe" e "senha errada":
            //    não revela quais e-mails existem.
            UsuarioMeiModel? usuario = _usuarioMeiRepository.BuscarPorEmail(email);
            if (usuario == null || !SenhaHasher.Verificar(login.Senha, usuario.SenhaHash))
            {
                return null;
            }

            // Senha correta, mas o cadastro ainda não foi liberado (só avisa quem provou ser o dono).
            if (usuario.Status == StatusCadastro.Pendente)
            {
                throw new UnauthorizedAccessException(
                    "Seu cadastro ainda está em análise. Você poderá entrar assim que um administrador aprová-lo.");
            }

            if (usuario.Status == StatusCadastro.Rejeitado)
            {
                string motivo = string.IsNullOrWhiteSpace(usuario.MotivoRejeicao) ? "" : $" Motivo: {usuario.MotivoRejeicao}";
                throw new UnauthorizedAccessException("Seu cadastro foi rejeitado." + motivo);
            }

            return new UsuarioMeiLoginModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Plano = usuario.Plano,
                Perfil = "mei"
            };
        }

        public List<UsuarioMeiModel> BuscarCadastros(string? status)
        {
            status = string.IsNullOrWhiteSpace(status) || status.Trim().ToLowerInvariant() == "todos"
                ? null
                : status.Trim().ToLowerInvariant();

            if (status != null && !StatusCadastro.Valido(status))
            {
                throw new ArgumentException("Status inválido. Use pendente, aprovado, rejeitado ou todos.");
            }

            return _usuarioMeiRepository.BuscarPorStatus(status);
        }

        public UsuarioMeiModel Aprovar(int Id)
        {
            return _usuarioMeiRepository.DefinirStatus(Id, StatusCadastro.Aprovado, null);
        }

        public UsuarioMeiModel Rejeitar(int Id, string motivo)
        {
            motivo = (motivo ?? string.Empty).Trim();

            if (motivo.Length == 0)
            {
                throw new ArgumentException("Informe o motivo da rejeição.");
            }

            if (motivo.Length > 300)
            {
                throw new ArgumentException("O motivo deve ter no máximo 300 caracteres.");
            }

            return _usuarioMeiRepository.DefinirStatus(Id, StatusCadastro.Rejeitado, motivo);
        }

        private static void ValidarSenha(string? senha)
        {
            if (string.IsNullOrEmpty(senha) || senha.Length < 8)
            {
                throw new ArgumentException("A senha deve ter pelo menos 8 caracteres.");
            }
        }

        // Limpa e valida os dados. Lança ArgumentException quando algo está inválido.
        private static void Normalizar(UsuarioMeiDataModel d)
        {
            d.Nome = d.Nome.Trim();
            d.Email = d.Email.Trim().ToLowerInvariant();
            d.Cpf = SoDigitos(d.Cpf);
            d.Cnpj = SoDigitos(d.Cnpj);
            d.Telefone = SoDigitos(d.Telefone);
            d.Cidade = d.Cidade.Trim();
            d.Estado = d.Estado.Trim().ToUpperInvariant();
            d.RamoAtividade = d.RamoAtividade.Trim();
            d.Descricao = d.Descricao.Trim();
            d.Experiencia = (d.Experiencia ?? string.Empty).Trim();
            d.Portfolio = (d.Portfolio ?? string.Empty).Trim();
            d.Plano = string.IsNullOrWhiteSpace(d.Plano) ? "gratuito" : d.Plano.Trim().ToLowerInvariant();

            // Categorias: sem repetição e sem vazios, em minúsculas.
            d.Categorias = string.Join(",",
                (d.Categorias ?? string.Empty)
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(c => c.ToLowerInvariant())
                    .Distinct());

            if (d.Nome.Length == 0) throw new ArgumentException("O nome é obrigatório.");
            if (d.Cpf.Length != 11) throw new ArgumentException("CPF inválido: informe 11 dígitos.");
            if (d.Cnpj.Length != 14) throw new ArgumentException("CNPJ inválido: informe 14 dígitos.");
            if (d.Telefone.Length < 10 || d.Telefone.Length > 11) throw new ArgumentException("Telefone inválido.");
            if (d.Categorias.Length == 0) throw new ArgumentException("Selecione pelo menos uma categoria.");

            if (d.Portfolio.Length > 0)
            {
                bool linkValido = Uri.TryCreate(d.Portfolio, UriKind.Absolute, out Uri? uri)
                    && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);

                if (!linkValido)
                {
                    throw new ArgumentException("O portfólio deve ser um link http(s) válido.");
                }
            }
        }

        private static string SoDigitos(string? texto)
        {
            return Regex.Replace(texto ?? string.Empty, @"\D", string.Empty);
        }
    }
}