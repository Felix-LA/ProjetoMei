using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using ProjetoMei.Front.Models;
using ProjetoMei.Front.Services;

namespace ProjetoMei.Front.Controllers
{
    /// <summary>Usuários: listagem, busca por nome, edição do nome e exclusão (pages/usuario/*.php).</summary>
    public class UsuariosController : BaseController
    {
        private readonly ApiClient _api;

        public UsuariosController(ApiClient api)
        {
            _api = api;
        }

        [HttpGet("usuarios")]
        public async Task<IActionResult> Index(string? nome)
        {
            string busca = (nome ?? string.Empty).Trim();
            var usuarios = new List<UsuarioApi>();
            string? erroApi = null;

            ApiResposta resposta = busca != string.Empty
                ? await _api.GetAsync("nome/" + Uri.EscapeDataString(busca))
                : await _api.GetAsync();

            if (resposta.Status == 404)
            {
                // nada encontrado
            }
            else if (!resposta.Ok)
            {
                erroApi = resposta.Mensagem;
            }
            else if (busca != string.Empty)
            {
                // a busca por nome devolve um único objeto
                UsuarioApi? encontrado = resposta.Dados<UsuarioApi>();
                if (encontrado != null && encontrado.Id > 0) usuarios.Add(encontrado);
            }
            else
            {
                usuarios = resposta.Dados<List<UsuarioApi>>() ?? new List<UsuarioApi>();
            }

            return View(new UsuariosViewModel(busca, usuarios, erroApi));
        }

        /// <summary>
        /// Edita o nome de um usuário já cadastrado. A API exige todos os campos no PUT: buscamos o
        /// cadastro atual, trocamos o nome e reenviamos tudo. Novos usuários entram pelo cadastro de MEI.
        /// </summary>
        [HttpPost("usuarios/salvar")]
        public async Task<IActionResult> Salvar(int id, string? nome)
        {
            nome = (nome ?? string.Empty).Trim();

            if (id <= 0)
            {
                Flash("erro", "Para cadastrar um novo MEI, use a página de cadastro.");
                return RedirectToAction(nameof(Index));
            }

            if (nome == string.Empty || nome.Length > 100)
            {
                Flash("erro", "Informe um nome com até 100 caracteres.");
                return RedirectToAction(nameof(Index));
            }

            ApiResposta atual = await _api.GetAsync(id.ToString());
            if (!atual.Ok || JsonNode.Parse(atual.Corpo) is not JsonObject corpo)
            {
                Flash("erro", atual.Status == 404 ? "Usuário não encontrado." : atual.Mensagem);
                return RedirectToAction(nameof(Index));
            }

            corpo["nome"] = nome;

            ApiResposta resposta = await _api.PutAsync(id.ToString(), corpo);

            if (resposta.Ok) Flash("sucesso", "Usuário atualizado com sucesso.");
            else Flash("erro", resposta.Mensagem);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("usuarios/excluir")]
        public async Task<IActionResult> Excluir(int id)
        {
            if (id <= 0)
            {
                Flash("erro", "Usuário inválido.");
                return RedirectToAction(nameof(Index));
            }

            ApiResposta resposta = await _api.DeleteAsync(id.ToString());

            if (resposta.Ok) Flash("sucesso", "Usuário excluído com sucesso.");
            else Flash("erro", resposta.Mensagem);

            return RedirectToAction(nameof(Index));
        }
    }
}
