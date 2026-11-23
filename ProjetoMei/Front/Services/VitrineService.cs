using System.Text.RegularExpressions;
using ProjetoMei.Front.Data;
using ProjetoMei.Front.Models;

namespace ProjetoMei.Front.Services
{
    /// <summary>Busca os MEIs na API e deixa cada um no formato do card (vitrine_buscar() do PHP).</summary>
    public class VitrineService
    {
        private static readonly Dictionary<string, string> Experiencias = new()
        {
            ["0-1"] = "Menos de 1 ano",
            ["1-3"] = "1 a 3 anos",
            ["3-5"] = "3 a 5 anos",
            ["5-10"] = "5 a 10 anos",
            ["10+"] = "Mais de 10 anos"
        };

        // "⚡ Eletricista" -> "Eletricista"
        private static readonly Dictionary<string, string> Rotulos = CadastroDados.Categorias
            .ToDictionary(c => c.Key, c => Regex.Replace(c.Value, @"^[^\p{L}]+", string.Empty));

        private readonly ApiClient _api;

        public VitrineService(ApiClient api)
        {
            _api = api;
        }

        /// <summary>Rótulo sem emoji de uma categoria do cadastro (ou a própria chave, capitalizada).</summary>
        public static string RotuloCategoria(string chave) =>
            Rotulos.TryGetValue(chave, out string? r) ? r : (chave.Length > 0 ? char.ToUpper(chave[0]) + chave[1..] : chave);

        public async Task<VitrineResultado> BuscarAsync()
        {
            ApiResposta resposta = await _api.GetAsync("vitrine");

            List<VitrineApi>? lista = resposta.Ok ? resposta.Dados<List<VitrineApi>>() : null;
            if (lista == null)
            {
                return new VitrineResultado(false, Array.Empty<MeiCard>(), resposta.Mensagem);
            }

            var itens = lista.Select(m =>
            {
                string[] partes = m.Nome.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string iniciais = partes.Length == 0
                    ? "?"
                    : partes[0][..1].ToUpper() + (partes.Length > 1 ? partes[^1][..1].ToUpper() : string.Empty);

                string local = string.IsNullOrEmpty(m.Estado) ? m.Cidade : $"{m.Cidade} - {m.Estado}";

                return new MeiCard(
                    Id: m.Id,
                    Nome: m.Nome,
                    Iniciais: iniciais,
                    Ramo: m.RamoAtividade,
                    Descricao: m.Descricao,
                    Local: local.Trim(),
                    Experiencia: Experiencias.TryGetValue(m.Experiencia, out string? exp) ? exp : string.Empty,
                    Portfolio: m.Portfolio,
                    Premium: m.Plano == "premium",
                    Chaves: m.Categorias,
                    Tags: m.Categorias.Select(RotuloCategoria).ToList());
            }).ToList();

            return new VitrineResultado(true, itens, null);
        }
    }
}
