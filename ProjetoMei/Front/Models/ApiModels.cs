namespace ProjetoMei.Front.Models
{
    // Formatos trocados com a API (JSON em camelCase).

    public class VitrineApi
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string RamoAtividade { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Experiencia { get; set; } = string.Empty;
        public string Portfolio { get; set; } = string.Empty;
        public List<string> Categorias { get; set; } = new();
        public string Plano { get; set; } = "gratuito";
    }

    public class UsuarioApi
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Plano { get; set; } = "gratuito";
    }

    public class LoginApi
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Plano { get; set; } = "gratuito";
        public string Perfil { get; set; } = "mei";   // "mei" ou "admin"
    }

    /// <summary>Cadastro de MEI como o administrador enxerga (todos os dados + situação).</summary>
    public class CadastroAdminApi
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string RamoAtividade { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Experiencia { get; set; } = string.Empty;
        public string Portfolio { get; set; } = string.Empty;
        public string Categorias { get; set; } = string.Empty;
        public string Plano { get; set; } = "gratuito";
        public string Status { get; set; } = "pendente";
        public string? MotivoRejeicao { get; set; }
        public DateTime? DataAnalise { get; set; }
        public DateTime DataCadastro { get; set; }
    }

    public record RejeitarRequisicao(string Motivo);

    /// <summary>Corpo do POST de cadastro. As propriedades viram camelCase no JSON.</summary>
    public record CadastroApi(
        string Nome, string Cpf, string Cnpj, string Email, string Senha, string Telefone,
        string Cidade, string Estado, string RamoAtividade, string Descricao,
        string Experiencia, string Portfolio, string Categorias, string Plano);

    public record LoginRequisicao(string Email, string Senha);

    /// <summary>Um MEI pronto para o card da vitrine.</summary>
    public record MeiCard(
        int Id, string Nome, string Iniciais, string Ramo, string Descricao, string Local,
        string Experiencia, string Portfolio, bool Premium,
        IReadOnlyList<string> Chaves, IReadOnlyList<string> Tags);

    public record VitrineResultado(bool Ok, IReadOnlyList<MeiCard> Itens, string? Erro);

    /// <summary>Usuário guardado na sessão depois do login.</summary>
    public record SessaoUsuario(int Id, string Nome, string Email, string Plano, string Perfil = "mei")
    {
        public bool EhAdmin => Perfil == "admin";
        public string PrimeiroNome => Nome.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? Nome;
    }
}