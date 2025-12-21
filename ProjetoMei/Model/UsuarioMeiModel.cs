using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMei.Model
{
    public class UsuarioMeiModel
    {
        [Key]
        public int Id {get; set;}

        [Required(ErrorMessage = "O Nome é Obrigatório")]
        [MaxLength(50, ErrorMessage = "O nome deve ter no máximo 50 caractere")]        
        public string Nome {get ;set;}
    }
}