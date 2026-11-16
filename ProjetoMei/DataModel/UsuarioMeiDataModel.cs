using System.ComponentModel.DataAnnotations;

namespace ProjetoMei.DataModel
{
    /// <summary>Dados recebidos do cliente (cadastro e atualização).</summary>
    public class UsuarioMeiDataModel
    {
        // Ignorado: quem gera o Id é o banco.
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [MaxLength(14, ErrorMessage = "CPF inválido.")]
        public string Cpf { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        [MaxLength(18, ErrorMessage = "CNPJ inválido.")]
        public string Cnpj { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [MaxLength(20, ErrorMessage = "Telefone inválido.")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [MaxLength(80)]
        public string Cidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "O estado é obrigatório.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Use a sigla do estado (2 letras).")]
        public string Estado { get; set; } = string.Empty;

        [Required(ErrorMessage = "O ramo de atividade é obrigatório.")]
        [MaxLength(100)]
        public string RamoAtividade { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [MaxLength(1000, ErrorMessage = "A descrição deve ter no máximo 1000 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [MaxLength(30)] public string Experiencia { get; set; } = string.Empty;
        [MaxLength(300)] public string Portfolio { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione pelo menos uma categoria.")]
        [MaxLength(300)]
        public string Categorias { get; set; } = string.Empty;

        [RegularExpression("^(gratuito|premium)$", ErrorMessage = "Plano inválido.")]
        public string Plano { get; set; } = "gratuito";
    }
}