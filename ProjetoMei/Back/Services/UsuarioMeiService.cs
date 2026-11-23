using System.Security.Cryptography;
using System.Text.RegularExpressions;
using ProjetoMei.Back.Models;
using ProjetoMei.Back.Repository;

namespace ProjetoMei.Back.Services
{
    public class UsuarioMeiService : IUsuarioMeiService
    {
        private readonly IUsuarioMeiRepository _usuarioMeiRepository;

        public UsuarioMeiService(IUsuarioMeiRepository usuarioMeiRepository)
        {
            _usuarioMeiRepository = usuarioMeiRepository;
        }

        public UsuarioMeiModel Adicionar(UsuarioMeiDataModel usuarioMeiDataModel)
        {
            ArgumentNullException.ThrowIfNull(usuarioMeiDataModel);

            Normalizar(usuarioMeiDataModel);
            ValidarSenha(usuarioMeiDataModel.Senha);

            if (_usuarioMeiRepository.ExisteCpfOuEmail(usuarioMeiDataModel.Cpf, usuarioMeiDataModel.Email))
            {
                throw new InvalidOperationException("Já existe um cadastro com este CPF ou e-mail.");
            }

            return _usuarioMeiRepository.Adicionar(usuarioMeiDataModel, GerarHash(usuarioMeiDataModel.Senha));
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
                novoHash = GerarHash(usuarioMeiDataModel.Senha);
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

            UsuarioMeiModel? usuario = _usuarioMeiRepository.BuscarPorEmail(login.Email.Trim().ToLowerInvariant());

            // Mesma resposta para "e-mail não existe" e "senha errada" (não revela quais e-mails existem).
            if (usuario == null || !VerificarHash(login.Senha, usuario.SenhaHash))
            {
                return null;
            }

            return new UsuarioMeiLoginModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Plano = usuario.Plano
            };
        }

        private static void ValidarSenha(string? senha)
        {
            if (string.IsNullOrEmpty(senha) || senha.Length < 8)
            {
                throw new ArgumentException("A senha deve ter pelo menos 8 caracteres.");
            }
        }

        // Formato guardado: v1.{iterações}.{salt em base64}.{hash em base64}
        private const int Iteracoes = 100_000;

        private static string GerarHash(string senha)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(senha, salt, Iteracoes, HashAlgorithmName.SHA256, 32);

            return $"v1.{Iteracoes}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        private static bool VerificarHash(string senha, string guardado)
        {
            string[] partes = (guardado ?? string.Empty).Split('.');
            if (partes.Length != 4 || partes[0] != "v1" || !int.TryParse(partes[1], out int iteracoes))
            {
                return false;
            }

            try
            {
                byte[] salt = Convert.FromBase64String(partes[2]);
                byte[] esperado = Convert.FromBase64String(partes[3]);
                byte[] calculado = Rfc2898DeriveBytes.Pbkdf2(senha, salt, iteracoes, HashAlgorithmName.SHA256, esperado.Length);

                return CryptographicOperations.FixedTimeEquals(calculado, esperado);
            }
            catch (FormatException)
            {
                return false;
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
