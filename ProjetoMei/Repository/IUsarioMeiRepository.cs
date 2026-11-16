using ProjetoMei.DataModel;
using ProjetoMei.Model;

namespace ProjetoMei.Interfaces
{
    /// <summary>
    /// Contrato de acesso aos dados de usuários MEI.
    /// Os métodos que buscam um usuário específico lançam <see cref="KeyNotFoundException"/>
    /// quando ele não existe.
    /// </summary>
    public interface IUsuarioMeiRepository
    {
        List<UsuarioMeiModel> BuscarUsuarios();
        UsuarioMeiModel BuscarUsuarioPorId(int Id);
        UsuarioMeiModel BuscarUsuarioPorNome(string Nome);
        UsuarioMeiModel Adicionar(UsuarioMeiDataModel usuarioMeiDataModel);
        UsuarioMeiModel Atualizar(UsuarioMeiDataModel usuarioMeiDataModel, int Id);
        bool Deletar(int Id);
        List<UsuarioMeiModel> BuscarVitrine();
        bool ExisteCpfOuEmail(string cpf, string email, int ignorarId = 0);
    }
}