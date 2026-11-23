using Microsoft.AspNetCore.Mvc;

namespace ProjetoMei.Front.Controllers
{
    /// <summary>Contato: cartões, formulário (ainda sem envio), horários e perguntas frequentes.</summary>
    public class ContatoController : BaseController
    {
        [HttpGet("contato")]
        public IActionResult Index() => View();
    }
}
