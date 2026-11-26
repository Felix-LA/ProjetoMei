using System.ComponentModel.DataAnnotations;

namespace ProjetoMei.Back.Models
{
    /// <summary>Corpo do pedido de rejeição de um cadastro.</summary>
    public class RejeitarDataModel
    {
        [Required(ErrorMessage = "Informe o motivo da rejeição.")]
        [MaxLength(300, ErrorMessage = "O motivo deve ter no máximo 300 caracteres.")]
        public string Motivo { get; set; } = string.Empty;
    }
}