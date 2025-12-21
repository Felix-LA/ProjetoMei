using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using ProjetoMei.Data;
using ProjetoMei.Model;

namespace ProjetoMei.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioMeiController : ControllerBase
    {
        private readonly MeiDbContext _meiDbContext;

        public UsuarioMeiController(MeiDbContext meiDbContext)
        {
            _meiDbContext = meiDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarUsuario(UsuarioMeiModel usuarioMeiModel)
        {
            _meiDbContext.Meis.Add(usuarioMeiModel);
            await _meiDbContext.SaveChangesAsync();

            return Ok("Usuário Cadastrado com Sucesso");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioMeiModel>>> BuscarUsuarios()
        {
            var usuarios = await _meiDbContext.Meis.ToListAsync();

            return usuarios;
        }

        [HttpGet("Id")]
        public async Task<IActionResult> BuscarUsuarioPorId (int id)
        {
            var usuario = await _meiDbContext.Meis.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuário Não Encontrado");
            }

            return Ok(usuario);
        }

        [HttpGet("Nome")]
        public async Task<IActionResult> BuscarUsuarioPorNome (string nome)
        {
            var Usuario = await _meiDbContext.Meis.FirstOrDefaultAsync(usuario => usuario.Nome.ToLower() == nome.ToLower());
            if (Usuario == null)
            {
                return NotFound("Usuário Não Encontrado");
            }

            return Ok(Usuario);
        }

        
    }
}