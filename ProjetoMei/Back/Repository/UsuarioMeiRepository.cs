using Microsoft.EntityFrameworkCore;
using ProjetoMei.Back.Data;
using ProjetoMei.Back.Models;

namespace ProjetoMei.Back.Repository
{
    public class UsuarioMeiRepository : IUsuarioMeiRepository
    {
        private readonly MeiDbContext _meiDbContext;

        public UsuarioMeiRepository(MeiDbContext meiDbContext)
        {
            _meiDbContext = meiDbContext;
        }

        public UsuarioMeiModel Adicionar(UsuarioMeiDataModel usuarioMeiDataModel, string senhaHash)
        {
            // O Id não é copiado: quem gera é o banco.
            UsuarioMeiModel usuarioMeiModel = new UsuarioMeiModel
            {
                DataCadastro = DateTime.UtcNow,
                SenhaHash = senhaHash
            };
            Copiar(usuarioMeiDataModel, usuarioMeiModel);

            _meiDbContext.Meis.Add(usuarioMeiModel);
            _meiDbContext.SaveChanges();

            return usuarioMeiModel;
        }

        public List<UsuarioMeiModel> BuscarUsuarios()
        {
            return _meiDbContext.Meis
                .AsNoTracking()
                .ToList();
        }

        public UsuarioMeiModel BuscarUsuarioPorId(int Id)
        {
            UsuarioMeiModel? usuario = _meiDbContext.Meis.Find(Id);

            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            return usuario;
        }

        public UsuarioMeiModel BuscarUsuarioPorNome(string Nome)
        {
            string termo = Nome.ToLower();

            UsuarioMeiModel? usuario = _meiDbContext.Meis
                .AsNoTracking()
                .FirstOrDefault(u => u.Nome.ToLower().Contains(termo));

            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            return usuario;
        }

        public UsuarioMeiModel Atualizar(UsuarioMeiDataModel usuarioMeiDataModel, int Id, string? senhaHash = null)
        {
            // A entidade carregada já é rastreada pelo EF: basta alterar e salvar.
            UsuarioMeiModel? usuario = _meiDbContext.Meis.Find(Id);

            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            Copiar(usuarioMeiDataModel, usuario);

            if (!string.IsNullOrEmpty(senhaHash))
            {
                usuario.SenhaHash = senhaHash;
            }

            _meiDbContext.SaveChanges();

            return usuario;
        }

        public bool Deletar(int Id)
        {
            UsuarioMeiModel? usuario = _meiDbContext.Meis.Find(Id);

            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            _meiDbContext.Meis.Remove(usuario);
            _meiDbContext.SaveChanges();

            return true;
        }

        public List<UsuarioMeiModel> BuscarVitrine()
        {
            // Só cadastros aprovados. Premium primeiro; dentro de cada plano, os mais recentes.
            return _meiDbContext.Meis
                .AsNoTracking()
                .Where(u => u.Status == StatusCadastro.Aprovado)
                .OrderByDescending(u => u.Plano == "premium")
                .ThenByDescending(u => u.DataCadastro)
                .ToList();
        }

        public List<UsuarioMeiModel> BuscarPorStatus(string? status)
        {
            IQueryable<UsuarioMeiModel> consulta = _meiDbContext.Meis.AsNoTracking();

            if (!string.IsNullOrEmpty(status))
            {
                consulta = consulta.Where(u => u.Status == status);
            }

            // Mais recentes primeiro.
            return consulta.OrderByDescending(u => u.DataCadastro).ToList();
        }

        public UsuarioMeiModel DefinirStatus(int Id, string status, string? motivo)
        {
            UsuarioMeiModel? usuario = _meiDbContext.Meis.Find(Id);

            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            usuario.Status = status;
            usuario.MotivoRejeicao = status == StatusCadastro.Rejeitado ? motivo : null;
            usuario.DataAnalise = DateTime.UtcNow;

            _meiDbContext.SaveChanges();

            return usuario;
        }

        public UsuarioMeiModel? BuscarPorEmail(string email)
        {
            return _meiDbContext.Meis
                .AsNoTracking()
                .FirstOrDefault(u => u.Email == email);
        }

        public bool ExisteCpfOuEmail(string cpf, string email, int ignorarId = 0)
        {
            return _meiDbContext.Meis
                .AsNoTracking()
                .Any(u => u.Id != ignorarId && (u.Cpf == cpf || u.Email == email));
        }

        // Copia os campos editáveis (nunca o Id, a data de cadastro nem a senha).
        private static void Copiar(UsuarioMeiDataModel origem, UsuarioMeiModel destino)
        {
            destino.Nome = origem.Nome;
            destino.Cpf = origem.Cpf;
            destino.Cnpj = origem.Cnpj;
            destino.Email = origem.Email;
            destino.Telefone = origem.Telefone;
            destino.Cidade = origem.Cidade;
            destino.Estado = origem.Estado;
            destino.RamoAtividade = origem.RamoAtividade;
            destino.Descricao = origem.Descricao;
            destino.Experiencia = origem.Experiencia;
            destino.Portfolio = origem.Portfolio;
            destino.Categorias = origem.Categorias;
            destino.Plano = origem.Plano;
        }
    }
}