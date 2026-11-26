using Microsoft.AspNetCore.Mvc;
using ProjetoMei.Back.Filters;
using ProjetoMei.Back.Models;
using ProjetoMei.Back.Services;

namespace ProjetoMei.Back.Controllers
{
    /// <summary>
    /// Análise dos cadastros de MEI pelo administrador. Só o site (Front) chama estes endpoints,
    /// e ele só os expõe a quem entrou como administrador.
    /// </summary>
    [ApiController]
    [IgnoreAntiforgeryToken]
    [ChaveApi]
    [Route("api/Admin")]
    public class AdminCadastrosController : ControllerBase
    {
        private readonly IUsuarioMeiService _usuarioMeiService;

        public AdminCadastrosController(IUsuarioMeiService usuarioMeiService)
        {
            _usuarioMeiService = usuarioMeiService;
        }

        // GET api/Admin/cadastros?status=pendente|aprovado|rejeitado|todos
        [HttpGet("cadastros")]
        [ProducesResponseType(typeof(IEnumerable<UsuarioMeiModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<UsuarioMeiModel>> Listar([FromQuery] string? status)
        {
            try
            {
                return Ok(_usuarioMeiService.BuscarCadastros(status));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        // POST api/Admin/cadastros/5/aprovar
        [HttpPost("cadastros/{id:int:min(1)}/aprovar")]
        [ProducesResponseType(typeof(UsuarioMeiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UsuarioMeiModel> Aprovar(int id)
        {
            try
            {
                return Ok(_usuarioMeiService.Aprovar(id));
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { mensagem = "Usuário não encontrado." });
            }
        }

        // POST api/Admin/cadastros/5/rejeitar   { "motivo": "..." }
        [HttpPost("cadastros/{id:int:min(1)}/rejeitar")]
        [ProducesResponseType(typeof(UsuarioMeiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UsuarioMeiModel> Rejeitar(int id, [FromBody] RejeitarDataModel dados)
        {
            try
            {
                return Ok(_usuarioMeiService.Rejeitar(id, dados.Motivo));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { mensagem = "Usuário não encontrado." });
            }
        }
    }
}