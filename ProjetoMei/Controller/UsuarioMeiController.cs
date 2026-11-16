using Microsoft.AspNetCore.Mvc;
using ProjetoMei.DataModel;
using ProjetoMei.Interfaces;
using ProjetoMei.Model;

namespace ProjetoMei.Controllers
{
    [ApiController]
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

        // GET api/UsuarioMei/vitrine  (dados públicos, sem CPF/CNPJ/e-mail/telefone)
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
            List<UsuarioMeiModel> usuarioMeiModels = _usuarioMeiService.BuscarUsuarios();

            return Ok(usuarioMeiModels);
        }

        // GET api/UsuarioMei/5
        [HttpGet("{id:int:min(1)}")]
        [ProducesResponseType(typeof(UsuarioMeiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UsuarioMeiModel> BuscarUsuarioPorId(int id)
        {
            try
            {
                UsuarioMeiModel usuarioMeiModel = _usuarioMeiService.BuscarUsuarioPorId(id);

                return Ok(usuarioMeiModel);
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
                UsuarioMeiModel usuarioMeiModel = _usuarioMeiService.BuscarUsuarioPorNome(nome);

                return Ok(usuarioMeiModel);
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
        public ActionResult<UsuarioMeiModel> Atualizar(int id, [FromBody] UsuarioMeiDataModel usuarioMeiDataModel)
        {
            // O Id válido é o da rota, não o do corpo da requisição.
            usuarioMeiDataModel.Id = id;

            try
            {
                UsuarioMeiModel usuarioMeiModel = _usuarioMeiService.Atualizar(usuarioMeiDataModel, id);

                return Ok(usuarioMeiModel);
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
                bool deletado = _usuarioMeiService.Deletar(id);

                if (!deletado)
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