using Microsoft.AspNetCore.Mvc;
using ProjetoMei.Front.Data;
using ProjetoMei.Front.Models;
using ProjetoMei.Front.Services;

namespace ProjetoMei.Front.Controllers
{
    /// <summary>
    /// Categorias de profissionais MEI (Categoria.php).
    /// Aceita ?cat=slug para abrir já na categoria escolhida (links da página inicial).
    /// </summary>
    public class CategoriasController : BaseController
    {
        private readonly VitrineService _vitrine;

        public CategoriasController(VitrineService vitrine)
        {
            _vitrine = vitrine;
        }

        [HttpGet("categorias")]
        public async Task<IActionResult> Index(string? cat)
        {
            VitrineResultado vitrine = await _vitrine.BuscarAsync();

            // Categoria aberta: a do parâmetro ?cat=, se for válida; senão a primeira.
            int ativa = CategoriasDados.Detalhes.ToList().FindIndex(d => d.Slug == cat);

            return View(new CategoriasViewModel(vitrine.Itens.Count, ativa < 0 ? 0 : ativa));
        }
    }
}
