using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using ProjetoMei.Data;
using ProjetoMei.DataModel;
using ProjetoMei.Interfaces;
using ProjetoMei.Model;

namespace ProjetoMei.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioMeiController : ControllerBase
    {
        public readonly IUsuarioMeiService usuarioMeiService;
        public UsuarioMeiController(IUsuarioMeiService _usuarioMeiService)
        {
            usuarioMeiService = _usuarioMeiService;
        }

        
        [HttpPost("Adicionar Usuario")]
        public async Task<IActionResult> AdicionarUsuario(UsuarioMeiDataModel usuarioMeiDataModel)
        {
            UsuarioMeiModel usuarioMeiModel = usuarioMeiService.Adicionar(usuarioMeiDataModel);

            return Ok(usuarioMeiModel);
        }


        [HttpGet("Buscar Usuarios")]
        public async Task<ActionResult<IEnumerable<UsuarioMeiModel>>> BuscarUsuarios()
        {
            List<UsuarioMeiModel> usuarioMeiModels = usuarioMeiService.BuscarUsuarios();

            return Ok(usuarioMeiModels);
        }

        
        [HttpGet("Busca Por Id")]
        public async Task<IActionResult> BuscarUsuarioPorId (int Id)
        {
            UsuarioMeiModel usuarioMeiModel = usuarioMeiService.BuscarUsuarioPorId(Id);

            return Ok(usuarioMeiModel);
        }

        /*
        [HttpGet("Busca Por Nome")]
        public async Task<IActionResult> BuscarUsuarioPorNome (string nome)
        {
            UsuarioMeiModel usuarioMeiModel = usuarioMeiService.BuscarUsuarioPorNome(nome);

            return Ok(usuarioMeiModel);
        }

        */


        [HttpPut("Atualizar")]
        public IActionResult Atualizar(UsuarioMeiDataModel usuarioMeiDataModel, int Id) 
        { 
            usuarioMeiDataModel.Id = Id;
            UsuarioMeiModel usuarioMeiModel = usuarioMeiService.Atualizar(usuarioMeiDataModel, Id);

            return Ok(usuarioMeiDataModel);
        }

        [HttpDelete("Id")]
        public bool DeletarPorID(int Id)
        {
            return usuarioMeiService.Deletar(Id);
        }


    }
}