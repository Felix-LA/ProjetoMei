using Microsoft.AspNetCore.Mvc;

namespace ProjetoMei.Front.Controllers
{
    /// <summary>Base dos controllers: aviso (flash) que sobrevive ao redirecionamento.</summary>
    public abstract class BaseController : Controller
    {
        /// <param name="tipo">"sucesso" ou "erro"</param>
        protected void Flash(string tipo, string mensagem)
        {
            TempData["flash_tipo"] = tipo;
            TempData["flash_msg"] = mensagem;
        }
    }
}
