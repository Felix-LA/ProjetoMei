using Microsoft.AspNetCore.Mvc;
using ProjetoMei.Front.Models;
using ProjetoMei.Front.Services;

namespace ProjetoMei.Front.Controllers
{
    /// <summary>Painel do administrador: aceitar ou rejeitar os cadastros de MEI.</summary>
    public class AdminController : BaseController
    {
        // "../Admin/..." sobe de /api/UsuarioMei/ para /api/Admin/ (a API fica neste mesmo projeto).
        private const string RotaApi = "../Admin/cadastros";

        private static readonly string[] StatusValidos = { "pendente", "aprovado", "rejeitado", "todos" };

        private readonly ApiClient _api;

        public AdminController(ApiClient api)
        {
            _api = api;
        }

        [HttpGet("admin")]
        public async Task<IActionResult> Index(string? status)
        {
            if (ExigirAdmin() is IActionResult negado) return negado;

            status = StatusValidos.Contains(status) ? status! : "pendente";

            ApiResposta resposta = await _api.GetAsync(RotaApi + "?status=todos");

            if (!resposta.Ok)
            {
                return View(new AdminViewModel(status, new List<CadastroAdminApi>(), 0, 0, 0, resposta.Mensagem));
            }

            var todos = resposta.Dados<List<CadastroAdminApi>>() ?? new List<CadastroAdminApi>();

            var itens = status == "todos" ? todos : todos.Where(c => c.Status == status).ToList();

            return View(new AdminViewModel(
                status, itens,
                Pendentes: todos.Count(c => c.Status == "pendente"),
                Aprovados: todos.Count(c => c.Status == "aprovado"),
                Rejeitados: todos.Count(c => c.Status == "rejeitado"),
                ErroApi: null));
        }

        [HttpPost("admin/aprovar")]
        public async Task<IActionResult> Aprovar(int id, string? status)
        {
            if (ExigirAdmin() is IActionResult negado) return negado;

            ApiResposta resposta = await _api.PostAsync($"{RotaApi}/{id}/aprovar", new { });

            if (resposta.Ok) Flash("sucesso", "Cadastro aprovado. O MEI já aparece na vitrine e pode entrar na conta.");
            else Flash("erro", resposta.Status == 404 ? "Cadastro não encontrado." : resposta.Mensagem);

            return RedirectToAction(nameof(Index), new { status });
        }

        [HttpPost("admin/rejeitar")]
        public async Task<IActionResult> Rejeitar(int id, string? motivo, string? status)
        {
            if (ExigirAdmin() is IActionResult negado) return negado;

            motivo = (motivo ?? string.Empty).Trim();
            if (motivo == string.Empty)
            {
                Flash("erro", "Informe o motivo da rejeição.");
                return RedirectToAction(nameof(Index), new { status });
            }

            ApiResposta resposta = await _api.PostAsync($"{RotaApi}/{id}/rejeitar", new RejeitarRequisicao(motivo));

            if (resposta.Ok) Flash("sucesso", "Cadastro rejeitado.");
            else Flash("erro", resposta.Status == 404 ? "Cadastro não encontrado." : resposta.Mensagem);

            return RedirectToAction(nameof(Index), new { status });
        }
    }
}