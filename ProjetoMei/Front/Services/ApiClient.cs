using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ProjetoMei.Front.Services
{
    /// <summary>Resposta da API já "digerida": status, corpo e mensagem de erro legível.</summary>
    public class ApiResposta
    {
        private static readonly JsonSerializerOptions Opcoes = new(JsonSerializerDefaults.Web);

        /// <summary>Status HTTP. 0 = não conseguiu conectar na API.</summary>
        public int Status { get; init; }
        public string Corpo { get; init; } = string.Empty;
        public string? ErroConexao { get; init; }

        public bool Ok => Status is >= 200 and < 300;
        public bool SemConexao => Status == 0;

        /// <summary>Converte o corpo JSON em objeto (ou null se vazio/inválido).</summary>
        public T? Dados<T>()
        {
            if (string.IsNullOrWhiteSpace(Corpo)) return default;
            try { return JsonSerializer.Deserialize<T>(Corpo, Opcoes); }
            catch (JsonException) { return default; }
        }

        /// <summary>
        /// Mensagem para mostrar ao usuário: { "mensagem": "..." } (400/404/409/401 da nossa API)
        /// ou o dicionário "errors" da validação automática do ASP.NET.
        /// </summary>
        public string Mensagem
        {
            get
            {
                if (SemConexao) return "Não foi possível conectar à API. Tente novamente em instantes.";

                try
                {
                    using JsonDocument doc = JsonDocument.Parse(Corpo);
                    JsonElement raiz = doc.RootElement;

                    if (raiz.ValueKind == JsonValueKind.Object)
                    {
                        if (raiz.TryGetProperty("mensagem", out JsonElement m) && m.ValueKind == JsonValueKind.String)
                            return m.GetString() ?? string.Empty;

                        if (raiz.TryGetProperty("errors", out JsonElement erros) && erros.ValueKind == JsonValueKind.Object)
                        {
                            var msgs = new List<string>();
                            foreach (JsonProperty campo in erros.EnumerateObject())
                                foreach (JsonElement e in campo.Value.EnumerateArray())
                                    if (e.GetString() is string texto) msgs.Add(texto);

                            if (msgs.Count > 0) return string.Join(" ", msgs.Distinct());
                        }
                    }
                }
                catch (Exception ex) when (ex is JsonException or InvalidOperationException) { /* corpo vazio ou fora do formato: cai na mensagem padrão */ }

                return $"A API respondeu com erro {Status}.";
            }
        }
    }

    /// <summary>
    /// Fala com a API (substitui a função api() do Config.php).
    /// Os caminhos são relativos a Api:BaseUrl, ex.: "vitrine", "login", "3".
    /// </summary>
    public class ApiClient
    {
        private static readonly JsonSerializerOptions Opcoes = new(JsonSerializerDefaults.Web);
        private readonly HttpClient _http;

        public ApiClient(HttpClient http)
        {
            _http = http;
        }

        public Task<ApiResposta> GetAsync(string caminho = "") => EnviarAsync(HttpMethod.Get, caminho);
        public Task<ApiResposta> PostAsync(string caminho, object corpo) => EnviarAsync(HttpMethod.Post, caminho, corpo);
        public Task<ApiResposta> PutAsync(string caminho, object corpo) => EnviarAsync(HttpMethod.Put, caminho, corpo);
        public Task<ApiResposta> DeleteAsync(string caminho) => EnviarAsync(HttpMethod.Delete, caminho);

        private async Task<ApiResposta> EnviarAsync(HttpMethod metodo, string caminho, object? corpo = null)
        {
            using var requisicao = new HttpRequestMessage(metodo, caminho);
            requisicao.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            if (corpo != null)
            {
                string json = JsonSerializer.Serialize(corpo, Opcoes);
                requisicao.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            try
            {
                using HttpResponseMessage resposta = await _http.SendAsync(requisicao);
                string texto = await resposta.Content.ReadAsStringAsync();

                return new ApiResposta { Status = (int)resposta.StatusCode, Corpo = texto };
            }
            catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
            {
                return new ApiResposta { Status = 0, ErroConexao = ex.Message };
            }
        }
    }
}
