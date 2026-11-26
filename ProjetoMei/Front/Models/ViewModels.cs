namespace ProjetoMei.Front.Models
{
    public record HomeViewModel(
        VitrineResultado Vitrine, int Total, int Cidades, int CategoriasUso, int Premium,
        IReadOnlyList<MeiCard> Destaques);

    public record AbaProfissionais(string Chave, string Nome, string Icone, IReadOnlyList<MeiCard> Itens);

    public record ProfissionaisViewModel(
        VitrineResultado Vitrine, IReadOnlyList<AbaProfissionais> Abas, int AbaAtiva, string Localizacao);

    public record CategoriasViewModel(int Total, int Ativa);

    public record AdminViewModel(
        string Status, IReadOnlyList<CadastroAdminApi> Itens,
        int Pendentes, int Aprovados, int Rejeitados, string? ErroApi);

    public record UsuariosViewModel(string Busca, IReadOnlyList<UsuarioApi> Usuarios, string? ErroApi);

    public class LoginViewModel
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}