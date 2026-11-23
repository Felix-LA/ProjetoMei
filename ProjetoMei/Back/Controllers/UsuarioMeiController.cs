using Microsoft.AspNetCore.Mvc;
using ProjetoMei.Back.Models;
using ProjetoMei.Back.Services;

namespace ProjetoMei.Back.Controllers
{
    // A API é chamada pelo próprio site (HttpClient), sem o token CSRF dos formulários.
    [ApiController]
    [IgnoreAntiforgeryToken]
    [Route("api/[controller]")]
    public class UsuarioMeiController : ControllerBase
    {
        private readonly IUsuarioMeiService _usuarioMeiService;

        public UsuarioMeiController(IUsuarioMeiService usuarioMeiService)
        {
            _usuarioMeiService = usuarioMeiService;
        }

        // POST api/UsuarioMei
        [HttpPost]
        [ProducesResponseType(typeof(UsuarioMeiModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<UsuarioMeiModel> AdicionarUsuario([FromBody] UsuarioMeiDataModel usuarioMeiDataModel)
        {
            // O Id é sempre gerado pelo banco; ignora qualquer valor enviado pelo cliente.
            usuarioMeiDataModel.Id = 0;

            try
            {
                UsuarioMeiModel usuarioMeiModel = _usuarioMeiService.Adicionar(usuarioMeiDataModel);

                return CreatedAtAction(
                    nameof(BuscarUsuarioPorId),
                    new { id = usuarioMeiModel.Id },
                    usuarioMeiModel);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensagem = ex.Message });
            }
        }

        // POST api/UsuarioMei/login
        [HttpPost("login")]
        [ProducesResponseType(typeof(UsuarioMeiLoginModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<UsuarioMeiLoginModel> Login([FromBody] LoginDataModel login)
        {
            UsuarioMeiLoginModel? usuario = _usuarioMeiService.Autenticar(login);

            if (usuario == null)
            {
                return Unauthorized(new { mensagem = "E-mail ou senha incorretos." });
            }

            return Ok(usuario);
        }

        // GET api/UsuarioMei/vitrine
        [HttpGet("vitrine")]
        [ProducesResponseType(typeof(IEnumerable<UsuarioMeiVitrineModel>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UsuarioMeiVitrineModel>> BuscarVitrine()
        {
            return Ok(_usuarioMeiService.BuscarVitrine());
        }

        // GET api/UsuarioMei
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UsuarioMeiModel>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UsuarioMeiModel>> BuscarUsuarios()
        {
            return Ok(_usuarioMeiService.BuscarUsuarios());
        }

        // GET api/UsuarioMei/5
        [HttpGet("{id:int:min(1)}")]
        [ProducesResponseType(typeof(UsuarioMeiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UsuarioMeiModel> BuscarUsuarioPorId(int id)
        {
            try
            {
                return Ok(_usuarioMeiService.BuscarUsuarioPorId(id));
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { mensagem = "Usuário não encontrado." });
            }
        }

        // GET api/UsuarioMei/nome/joao
        [HttpGet("nome/{nome}")]
        [ProducesResponseType(typeof(UsuarioMeiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UsuarioMeiModel> BuscarUsuarioPorNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return BadRequest(new { mensagem = "Informe o nome para a busca." });
            }

            try
            {
                return Ok(_usuarioMeiService.BuscarUsuarioPorNome(nome));
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { mensagem = "Usuário não encontrado." });
            }
        }

        // PUT api/UsuarioMei/5
        [HttpPut("{id:int:min(1)}")]
        [ProducesResponseType(typeof(UsuarioMeiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<UsuarioMeiModel> Atualizar(int id, [FromBody] UsuarioMeiDataModel usuarioMeiDataModel)
        {
            // O Id válido é o da rota, não o do corpo da requisição.
            usuarioMeiDataModel.Id = id;

            try
            {
                return Ok(_usuarioMeiService.Atualizar(usuarioMeiDataModel, id));
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { mensagem = "Usuário não encontrado." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensagem = ex.Message });
            }
        }

        // DELETE api/UsuarioMei/5
        [HttpDelete("{id:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletarPorID(int id)
        {
            try
            {
                if (!_usuarioMeiService.Deletar(id))
                {
                    return NotFound(new { mensagem = "Usuário não encontrado." });
                }

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { mensagem = "Usuário não encontrado." });
            }
        }
    }
}
