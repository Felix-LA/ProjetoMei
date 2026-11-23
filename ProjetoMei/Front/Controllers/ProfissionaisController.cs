using Microsoft.AspNetCore.Mvc;
using ProjetoMei.Front.Data;
using ProjetoMei.Front.Models;
using ProjetoMei.Front.Services;

namespace ProjetoMei.Front.Controllers
{
    /// <summary>
    /// Listagem de profissionais em abas por categoria (Listagem.php).
    ///   ?categoria=eletricista   abre a aba dessa categoria (se houver MEIs nela)
    ///   ?localizacao=campo       mostra só MEIs cujo "cidade - UF" contém o texto
    /// </summary>
    public class ProfissionaisController : BaseController
    {
        private readonly VitrineService _vitrine;

        public ProfissionaisController(VitrineService vitrine)
        {
            _vitrine = vitrine;
        }

        [HttpGet("profissionais")]
        public async Task<IActionResult> Index(string? categoria, string? localizacao)
        {
            VitrineResultado vitrine = await _vitrine.BuscarAsync();
            string local = (localizacao ?? string.Empty).Trim();

            IReadOnlyList<MeiCard> itens = vitrine.Itens;
            if (local != string.Empty)
            {
                itens = itens
                    .Where(m => m.Local.Contains(local, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();
            }

            // Abas: "Todos" + uma por categoria em uso (na ordem em que aparecem no cadastro).
            var abas = new List<AbaProfissionais>();
            if (itens.Count > 0)
            {
                abas.Add(new AbaProfissionais(string.Empty, "Todos", "fa-layer-group", itens));

                foreach (var c in CadastroDados.Categorias)
                {
                    var doGrupo = itens.Where(m => m.Chaves.Contains(c.Key)).ToList();
                    if (doGrupo.Count > 0)
                    {
                        abas.Add(new AbaProfissionais(c.Key, VitrineService.RotuloCategoria(c.Key), "fa-tag", doGrupo));
                    }
                }
            }

            // Aba aberta: a da categoria pedida, se existir; senão "Todos".
            int ativa = string.IsNullOrEmpty(categoria) ? 0 : abas.FindIndex(a => a.Chave == categoria);

            return View(new ProfissionaisViewModel(vitrine, abas, ativa < 0 ? 0 : ativa, local));
        }
    }
}
