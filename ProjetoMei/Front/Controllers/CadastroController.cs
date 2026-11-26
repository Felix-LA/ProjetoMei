using Microsoft.AspNetCore.Mvc;
using ProjetoMei.Front.Models;
using ProjetoMei.Front.Services;

namespace ProjetoMei.Front.Controllers
{
    /// <summary>
    /// Cadastro de MEI em 4 passos (CadastroMei.php + CadastroMeiSalvar.php).
    /// Os documentos (RG, comprovante, foto) ainda NÃO são enviados: a API só recebe os dados de texto.
    /// </summary>
    public class CadastroController : BaseController
    {
        private readonly ApiClient _api;

        public CadastroController(ApiClient api)
        {
            _api = api;
        }

        [HttpGet("cadastro-mei")]
        public IActionResult Index() => View();

        /// <summary>Recebido por fetch (CadastroMei.js). Responde sempre JSON: { ok, mensagem }.</summary>
        [HttpPost("cadastro-mei/salvar")]
        public async Task<IActionResult> Salvar()
        {
            IFormCollection form = Request.Form;
            string Campo(string nome) => form[nome].ToString().Trim();

            // A senha não leva Trim: espaços podem fazer parte dela.
            string senha = form["senha"].ToString();

            if (senha != form["senha-confirmar"].ToString())
                return Resposta(422, false, "A confirmação da senha não confere.");

            if (string.IsNullOrEmpty(form["termos"]))
                return Resposta(422, false, "Você deve aceitar os Termos de Uso para continuar.");

            string plano = Campo("plano");

            var corpo = new CadastroApi(
                Nome: Campo("nome"),
                Cpf: Campo("cpf"),
                Cnpj: Campo("cnpj"),
                Email: Campo("email"),
                Senha: senha,
                Telefone: Campo("telefone"),
                Cidade: Campo("cidade"),
                Estado: Campo("estado"),
                RamoAtividade: Campo("ramo-atividade"),
                Descricao: Campo("descricao"),
                Experiencia: Campo("experiencia"),
                Portfolio: Campo("portfolio"),
                Categorias: Campo("categorias"),
                Plano: plano == "" ? "gratuito" : plano);

            ApiResposta resposta = await _api.PostAsync(string.Empty, corpo);

            if (resposta.Ok)
                return Resposta(201, true, "Cadastro enviado para análise.");

            if (resposta.SemConexao)
                return Resposta(503, false, "Não foi possível conectar à API. Tente novamente em instantes.");

            // 400 (validação) e 409 (CPF/e-mail já cadastrado) trazem a mensagem da API.
            int status = resposta.Status is 400 or 409 ? resposta.Status : 502;
            return Resposta(status, false, resposta.Mensagem);
        }

        private IActionResult Resposta(int status, bool ok, string mensagem) =>
            StatusCode(status, new { ok, mensagem });
    }
}