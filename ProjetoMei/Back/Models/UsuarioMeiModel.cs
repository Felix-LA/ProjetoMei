using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoMei.Back.Models
{
    /// <summary>Entidade salva no banco: um MEI cadastrado.</summary>
    public class UsuarioMeiModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome é Obrigatório")]
        [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres")]
        public string Nome { get; set; } = string.Empty;

        // Somente dígitos (a máscara é só do front).
        [MaxLength(11)] public string Cpf { get; set; } = string.Empty;
        [MaxLength(14)] public string Cnpj { get; set; } = string.Empty;
        [MaxLength(150)] public string Email { get; set; } = string.Empty;
        [MaxLength(11)] public string Telefone { get; set; } = string.Empty;

        [MaxLength(80)] public string Cidade { get; set; } = string.Empty;
        [MaxLength(2)] public string Estado { get; set; } = string.Empty;

        [MaxLength(100)] public string RamoAtividade { get; set; } = string.Empty;
        [MaxLength(1000)] public string Descricao { get; set; } = string.Empty;
        [MaxLength(30)] public string Experiencia { get; set; } = string.Empty;
        [MaxLength(300)] public string Portfolio { get; set; } = string.Empty;

        // Chaves separadas por vírgula, ex.: "eletricista,pintor".
        [MaxLength(300)] public string Categorias { get; set; } = string.Empty;

        // "gratuito" ou "premium".
        [MaxLength(20)] public string Plano { get; set; } = "gratuito";

        // Hash PBKDF2 da senha. [JsonIgnore]: nunca sai em nenhuma resposta da API.
        [JsonIgnore]
        [MaxLength(200)]
        public string SenhaHash { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    }
}
