using System.ComponentModel.DataAnnotations;

namespace ProjetoMei.DataModel
{
    public class UsuarioMeiDataModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        
    }
}
