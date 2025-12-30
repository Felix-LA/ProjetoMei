using ProjetoMei.Data;
using ProjetoMei.DataModel;
using ProjetoMei.Interfaces;
using ProjetoMei.Model;
using ProjetoMei.Repository;

namespace ProjetoMei.Services
{
    public class UsuarioMeiService : IUsuarioMeiService
    {
        public readonly IUsuarioMeiRepository iUsuarioMeiRepository;
        public UsuarioMeiService(IUsuarioMeiRepository _iusuarioMeiRepository)
        {
            iUsuarioMeiRepository = _iusuarioMeiRepository;
        }
        public UsuarioMeiModel Adicionar(UsuarioMeiDataModel usuarioMeiDataModel)
        {
            return iUsuarioMeiRepository.Adicionar(usuarioMeiDataModel);
        }

        public List<UsuarioMeiModel> BuscarUsuarios()
        {
            return iUsuarioMeiRepository.BuscarUsuarios();
        }

        public UsuarioMeiModel BuscarUsuarioPorId(int Id)
        {
            return iUsuarioMeiRepository.BuscarUsuarioPorId(Id);
        }
        /*
        public UsuarioMeiModel BuscarUsuarioPorNome(string Nome)
        {
            return iUsuarioMeiRepository.BuscarUsuarioPorNome(Nome);
        }
        */


        public UsuarioMeiModel Atualizar(MeiDbContext meiDbContext, int Id)
        {
            throw new NotImplementedException();
        }

        public bool Deletar(int Id)
        {
            return iUsuarioMeiRepository.Deletar(Id);
        }
    }
}
