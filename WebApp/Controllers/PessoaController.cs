using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApp.ViewModels; // Ajuste se necessário

namespace WebApp.Controllers
{
    public class PessoaController : Controller
    {
        private readonly HttpClient _httpClient;

        public PessoaController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient"); // Usa o client nomeado configurado no Program.cs
        }

        // Lista todas as pessoas consumindo API REST (GET /api/pessoa)
        public async Task<IActionResult> Index()
        {
            var pessoas = await _httpClient.GetFromJsonAsync<List<PessoaViewModel>>("/api/pessoa");
            return View(pessoas);
        }

        // Tela de cadastro
        public IActionResult Create()
        {
            return View();
        }

        // POST para criar pessoa (POST /api/pessoa)
        [HttpPost]
        public async Task<IActionResult> Create(PessoaViewModel pessoa)
        {
            if (!ModelState.IsValid)
                return View(pessoa);

            var response = await _httpClient.PostAsJsonAsync("/api/pessoa", pessoa);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Erro ao criar pessoa via API");
            return View(pessoa);
        }

        // Tela de edição (GET /api/pessoa/{id})
        public async Task<IActionResult> Edit(int id)
        {
            var pessoa = await _httpClient.GetFromJsonAsync<PessoaViewModel>($"/api/pessoa/{id}");
            if (pessoa == null)
                return NotFound();

            return View(pessoa);
        }

        // POST de edição (PUT /api/pessoa/{id})
        [HttpPost]
        public async Task<IActionResult> Edit(int id, PessoaViewModel pessoa)
        {
            if (id != pessoa.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(pessoa);

            var response = await _httpClient.PutAsJsonAsync($"/api/pessoa/{id}", pessoa);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Erro ao atualizar pessoa via API");
            return View(pessoa);
        }

        // Tela de exclusão (GET /api/pessoa/{id})
        public async Task<IActionResult> Delete(int id)
        {
            var pessoa = await _httpClient.GetFromJsonAsync<PessoaViewModel>($"/api/pessoa/{id}");
            if (pessoa == null)
                return NotFound();

            return View(pessoa);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/pessoa/{id}");

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index)); 

                
                var erro = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"Erro ao excluir: {response.StatusCode} - {erro}");

                var pessoa = await _httpClient.GetFromJsonAsync<PessoaViewModel>($"/api/pessoa/{id}");
                return View("Delete", pessoa);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro inesperado: {ex.Message}");
                var pessoa = await _httpClient.GetFromJsonAsync<PessoaViewModel>($"/api/pessoa/{id}");
                return View("Delete", pessoa);
            }
        }


    }
}
