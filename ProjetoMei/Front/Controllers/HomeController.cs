using Microsoft.AspNetCore.Mvc;
using ProjetoMei.Front.Models;
using ProjetoMei.Front.Services;

namespace ProjetoMei.Front.Controllers
{
    /// <summary>Página principal (Inicio.php).</summary>
    public class HomeController : BaseController
    {
        private readonly VitrineService _vitrine;

        public HomeController(VitrineService vitrine)
        {
            _vitrine = vitrine;
        }

        [HttpGet("/")]
        [HttpGet("inicio")]
        public async Task<IActionResult> Index()
        {
            VitrineResultado vitrine = await _vitrine.BuscarAsync();

            // Números reais, calculados a partir dos MEIs cadastrados na API.
            int total = vitrine.Itens.Count;
            int cidades = vitrine.Itens.Select(m => m.Local).Where(l => l != "").Distinct().Count();
            int categoriasEmUso = vitrine.Itens.SelectMany(m => m.Chaves).Distinct().Count();
            int premium = vitrine.Itens.Count(m => m.Premium);

            // Até 9 destaques (3 por slide do carrossel).
            var destaques = vitrine.Itens.Take(9).ToList();

            return View(new HomeViewModel(vitrine, total, cidades, categoriasEmUso, premium, destaques));
        }
    }
}
