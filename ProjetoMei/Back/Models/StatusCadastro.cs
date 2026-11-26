namespace ProjetoMei.Back.Models
{
    /// <summary>Situação de um cadastro de MEI, decidida por um administrador.</summary>
    public static class StatusCadastro
    {
        public const string Pendente = "pendente";
        public const string Aprovado = "aprovado";
        public const string Rejeitado = "rejeitado";

        public static bool Valido(string? status) =>
            status is Pendente or Aprovado or Rejeitado;
    }
}