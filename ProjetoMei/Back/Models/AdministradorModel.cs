using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoMei.Back.Models
{
    /// <summary>Conta de administrador: analisa (aceita ou rejeita) os cadastros de MEI.</summary>
    public class AdministradorModel
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [JsonIgnore]
        [Required, MaxLength(200)]
        public string SenhaHash { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    }
}