using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Text;

namespace WebApp.Controllers
{
    public class CandidaturaController : Controller
    {
        private readonly HttpClient _httpClient;

        public CandidaturaController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        private async Task PopularDropDownsAsync()
        {
            var pessoas = await _httpClient.GetFromJsonAsync<List<PessoaViewModel>>("/api/pessoa");
            var vagas = await _httpClient.GetFromJsonAsync<List<VagaViewModel>>("/api/vaga");

            ViewBag.Pessoas = pessoas.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nome }).ToList();
            ViewBag.VagasSelect = vagas.Select(v => new SelectListItem { Value = v.Id.ToString(), Text = v.Titulo }).ToList();
        }

        // GET: /Candidatura
        public async Task<IActionResult> Index(int? vagaId, string busca, bool? aprovado, int pagina = 1, int tamanhoPagina = 10)
        {
            var candidaturas = await _httpClient.GetFromJsonAsync<List<CandidaturaViewModel>>("/api/candidaturas");
            var pessoas = await _httpClient.GetFromJsonAsync<List<PessoaViewModel>>("/api/pessoa");
            var vagas = await _httpClient.GetFromJsonAsync<List<VagaViewModel>>("/api/vaga");

            // Preencher nomes e títulos
            foreach (var c in candidaturas)
            {
                c.PessoaNome = pessoas.FirstOrDefault(p => p.Id == c.PessoaId)?.Nome ?? "Desconhecido";
                c.VagaTitulo = vagas.FirstOrDefault(v => v.Id == c.VagaId)?.Titulo ?? "Desconhecida";
            }

            // Aplicar filtros
            if (vagaId.HasValue)
                candidaturas = candidaturas.Where(c => c.VagaId == vagaId.Value).ToList();

            if (aprovado.HasValue)
                candidaturas = candidaturas.Where(c => c.Aprovado == aprovado.Value).ToList();

            if (!string.IsNullOrWhiteSpace(busca))
                candidaturas = candidaturas.Where(c =>
                    (c.PessoaNome?.ToLower().Contains(busca.ToLower()) ?? false) ||
                    (c.VagaTitulo?.ToLower().Contains(busca.ToLower()) ?? false)).ToList();

            // Paginação
            int total = candidaturas.Count();
            int totalPaginas = (int)Math.Ceiling((double)total / tamanhoPagina);
            var paginadas = candidaturas
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToList();

            // ViewBags para filtros e dropdowns
            await PopularDropDownsAsync();
            ViewBag.VagaSelecionada = vagaId;
            ViewBag.Aprovado = aprovado;
            ViewBag.Busca = busca;
            ViewBag.PaginaAtual = pagina;
            ViewBag.TotalPaginas = totalPaginas;

            return View(paginadas);
        }

        // Exportação CSV
        [HttpGet]
        public async Task<IActionResult> ExportarCsv(int? vagaId, string busca, bool? aprovado)
        {
            var candidaturas = await _httpClient.GetFromJsonAsync<List<CandidaturaViewModel>>("/api/candidaturas");
            var pessoas = await _httpClient.GetFromJsonAsync<List<PessoaViewModel>>("/api/pessoa");
            var vagas = await _httpClient.GetFromJsonAsync<List<VagaViewModel>>("/api/vaga");

            foreach (var c in candidaturas)
            {
                c.PessoaNome = pessoas.FirstOrDefault(p => p.Id == c.PessoaId)?.Nome ?? "Desconhecido";
                c.VagaTitulo = vagas.FirstOrDefault(v => v.Id == c.VagaId)?.Titulo ?? "Desconhecida";
            }

            if (vagaId.HasValue)
                candidaturas = candidaturas.Where(c => c.VagaId == vagaId.Value).ToList();

            if (aprovado.HasValue)
                candidaturas = candidaturas.Where(c => c.Aprovado == aprovado.Value).ToList();

            if (!string.IsNullOrWhiteSpace(busca))
                candidaturas = candidaturas.Where(c =>
                    (c.PessoaNome?.ToLower().Contains(busca.ToLower()) ?? false) ||
                    (c.VagaTitulo?.ToLower().Contains(busca.ToLower()) ?? false)).ToList();

            var sb = new StringBuilder();
            sb.AppendLine("Id;Pessoa;Vaga;Aprovado");

            foreach (var c in candidaturas)
            {
                sb.AppendLine($"{c.Id};{c.PessoaNome};{c.VagaTitulo};{(c.Aprovado ? "Sim" : "Não")}");
            }

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "candidaturas.csv");
        }

        // Create
        public async Task<IActionResult> Create()
        {
            await PopularDropDownsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CandidaturaViewModel candidatura)
        {
            if (!ModelState.IsValid)
            {
                await PopularDropDownsAsync();
                return View(candidatura);
            }

            var response = await _httpClient.PostAsJsonAsync("/api/candidaturas", candidatura);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Erro ao criar candidatura.");
            await PopularDropDownsAsync();
            return View(candidatura);
        }

        // Edit
        public async Task<IActionResult> Edit(int id)
        {
            var candidatura = await _httpClient.GetFromJsonAsync<CandidaturaViewModel>($"/api/candidaturas/{id}");
            if (candidatura == null)
                return NotFound();

            await PopularDropDownsAsync();
            return View(candidatura);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CandidaturaViewModel candidatura)
        {
            if (id != candidatura.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                await PopularDropDownsAsync();
                return View(candidatura);
            }

            var response = await _httpClient.PutAsJsonAsync($"/api/candidaturas/{id}", candidatura);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Erro ao atualizar candidatura.");
            await PopularDropDownsAsync();
            return View(candidatura);
        }

        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            var candidatura = await _httpClient.GetFromJsonAsync<CandidaturaViewModel>($"/api/candidaturas/{id}");
            if (candidatura == null)
                return NotFound();

            return View(candidatura);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/candidaturas/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return BadRequest("Erro ao excluir candidatura.");
        }
    }
}
