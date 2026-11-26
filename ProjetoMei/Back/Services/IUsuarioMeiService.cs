using ProjetoMei.Back.Models;

namespace ProjetoMei.Back.Services
{
    /// <summary>
    /// Contrato das regras de negócio de usuários MEI.
    /// Os métodos que buscam um usuário específico lançam <see cref="KeyNotFoundException"/>
    /// quando ele não existe. Dados inválidos lançam <see cref="ArgumentException"/> e
    /// CPF/e-mail já cadastrados lançam <see cref="InvalidOperationException"/>.
    /// Login de cadastro pendente ou rejeitado lança <see cref="UnauthorizedAccessException"/>.
    /// </summary>
    public interface IUsuarioMeiService
    {
        List<UsuarioMeiModel> BuscarUsuarios();
        UsuarioMeiModel BuscarUsuarioPorId(int Id);
        UsuarioMeiModel BuscarUsuarioPorNome(string Nome);
        UsuarioMeiModel Adicionar(UsuarioMeiDataModel usuarioMeiDataModel);
        UsuarioMeiModel Atualizar(UsuarioMeiDataModel usuarioMeiDataModel, int Id);
        bool Deletar(int Id);
        List<UsuarioMeiVitrineModel> BuscarVitrine();
        UsuarioMeiLoginModel? Autenticar(LoginDataModel login);

        // ---- Painel do administrador ----
        List<UsuarioMeiModel> BuscarCadastros(string? status);
        UsuarioMeiModel Aprovar(int Id);
        UsuarioMeiModel Rejeitar(int Id, string motivo);
    }
}