namespace ProjetoMei.Back.Models
{
    /// <summary>O que o login devolve: só o necessário para a sessão do front.</summary>
    public class UsuarioMeiLoginModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Plano { get; set; } = "gratuito";
    }
}
