using ProjetoMei.Back.Models;

namespace ProjetoMei.Back.Repository
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
        UsuarioMeiModel Adicionar(UsuarioMeiDataModel usuarioMeiDataModel, string senhaHash);
        UsuarioMeiModel Atualizar(UsuarioMeiDataModel usuarioMeiDataModel, int Id, string? senhaHash = null);
        bool Deletar(int Id);
        List<UsuarioMeiModel> BuscarVitrine();
        UsuarioMeiModel? BuscarPorEmail(string email);
        bool ExisteCpfOuEmail(string cpf, string email, int ignorarId = 0);
    }
}
