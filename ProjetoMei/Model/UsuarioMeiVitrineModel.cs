namespace ProjetoMei.Model
{
    /// <summary>
    /// Visão pública de um MEI para a vitrine: sem CPF, CNPJ, e-mail e telefone.
    /// </summary>
    public class UsuarioMeiVitrineModel
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
        public DateTime DataCadastro { get; set; }
    }
}