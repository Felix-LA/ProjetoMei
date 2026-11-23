using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProjetoMei.Front.Models;
using ProjetoMei.Front.Services;

namespace ProjetoMei.Front.Controllers
{
    /// <summary>Login e logout (Login.php, Autenticar.php e Sair.php).</summary>
    public class ContaController : BaseController
    {
        private readonly ApiClient _api;

        public ContaController(ApiClient api)
        {
            _api = api;
        }

        [HttpGet("login")]
        public IActionResult Login() => View();

        /// <summary>Confere e-mail e senha na API (POST /login) e guarda o usuário na sessão.</summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login(string? email, string? senha)
        {
            email = (email ?? string.Empty).Trim();
            senha ??= string.Empty;

            if (!new EmailAddressAttribute().IsValid(email) || senha == string.Empty)
            {
                Flash("erro", "Informe e-mail e senha válidos.");
                return RedirectToAction(nameof(Login));
            }

            ApiResposta resposta = await _api.PostAsync("login", new LoginRequisicao(email, senha));

            LoginApi? usuario = resposta.Ok ? resposta.Dados<LoginApi>() : null;
            if (usuario != null && usuario.Id > 0)
            {
                HttpContext.EntrarNaSessao(new SessaoUsuario(usuario.Id, usuario.Nome, usuario.Email, usuario.Plano));
                Flash("sucesso", $"Bem-vindo(a), {usuario.Nome}!");
                return RedirectToAction("Index", "Home");
            }

            // 401 = e-mail ou senha incorretos; 0 = API fora do ar; o resto traz a mensagem da API.
            Flash("erro", resposta.Status == 401 ? "E-mail ou senha incorretos." : resposta.Mensagem);
            return RedirectToAction(nameof(Login));
        }

        [HttpGet("sair")]
        public IActionResult Sair()
        {
            HttpContext.SairDaSessao();
            Flash("sucesso", "Você saiu da sua conta.");
            return RedirectToAction("Index", "Home");
        }
    }
}
