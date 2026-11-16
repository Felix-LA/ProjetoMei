using System.Text.RegularExpressions;
using ProjetoMei.DataModel;
using ProjetoMei.Interfaces;
using ProjetoMei.Model;

namespace ProjetoMei.Services
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

            if (_usuarioMeiRepository.ExisteCpfOuEmail(usuarioMeiDataModel.Cpf, usuarioMeiDataModel.Email))
            {
                throw new InvalidOperationException("Já existe um cadastro com este CPF ou e-mail.");
            }

            return _usuarioMeiRepository.Adicionar(usuarioMeiDataModel);
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

            return _usuarioMeiRepository.Atualizar(usuarioMeiDataModel, Id);
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
                bool linkValido = Uri.TryCreate(d.Portfolio, UriKind.Absolute, out Uri? link)
                    && (link.Scheme == Uri.UriSchemeHttp || link.Scheme == Uri.UriSchemeHttps);

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