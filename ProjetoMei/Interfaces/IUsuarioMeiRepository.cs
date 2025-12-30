using ProjetoMei.Data;
using ProjetoMei.DataModel;
using ProjetoMei.Model;

namespace ProjetoMei.Interfaces
{
    public interface IUsuarioMeiRepository
    {
        List<UsuarioMeiModel> BuscarUsuarios();
        UsuarioMeiModel BuscarUsuarioPorId(int Id);
        //UsuarioMeiModel BuscarUsuarioPorNome( string Nome);
        UsuarioMeiModel Adicionar(UsuarioMeiDataModel usuarioMeiDataModel);
        UsuarioMeiModel Atualizar(UsuarioMeiDataModel usuarioMeiDataModel, int Id);
        bool Deletar(int Id);

    }
}
