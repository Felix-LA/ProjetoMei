using System.ComponentModel.DataAnnotations;

namespace ProjetoMei.Back.Models
{
    /// <summary>Credenciais enviadas no login.</summary>
    public class LoginDataModel
    {
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MaxLength(100)]
        public string Senha { get; set; } = string.Empty;
    }
}
