using Microsoft.AspNetCore.Mvc;
using ProjetoMei.Front.Services;

namespace ProjetoMei.Front.Controllers
{
    /// <summary>Base dos controllers: aviso (flash) que sobrevive ao redirecionamento.</summary>
    public abstract class BaseController : Controller
    {
        /// <summary>Devolve um redirecionamento se quem acessa não é administrador; senão, null.</summary>
        protected IActionResult? ExigirAdmin()
        {
            if (HttpContext.UsuarioLogado()?.EhAdmin == true) return null;

            Flash("erro", "Acesso restrito a administradores. Entre com uma conta de administrador.");
            return RedirectToAction("Login", "Conta");
        }

        /// <param name="tipo">"sucesso" ou "erro"</param>
        protected void Flash(string tipo, string mensagem)
        {
            TempData["flash_tipo"] = tipo;
            TempData["flash_msg"] = mensagem;
        }
    }
}