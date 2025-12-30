using ProjetoMei.Data;
using ProjetoMei.DataModel;
using ProjetoMei.Model;

namespace ProjetoMei.Interfaces
{
    public interface IUsuarioMeiService
    {
        List<UsuarioMeiModel> BuscarUsuarios();
        UsuarioMeiModel BuscarUsuarioPorId(int Id);
       // UsuarioMeiModel BuscarUsuarioPorNome(string Nome);
        UsuarioMeiModel Adicionar(UsuarioMeiDataModel usuarioMeiDataModel);
        UsuarioMeiModel Atualizar(MeiDbContext meiDbContext, int Id);
        bool Deletar(int Id);
    }
}
