using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class VagaController : Controller
    {
        private readonly HttpClient _httpClient;

        public VagaController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient"); // já configurado no Program.cs
        }

        // GET: /Vaga
        public async Task<IActionResult> Index()
        {
            var vagas = await _httpClient.GetFromJsonAsync<List<VagaViewModel>>("/api/vaga");
            return View(vagas);
        }

        // GET: /Vaga/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Vaga/Create
        [HttpPost]
        public async Task<IActionResult> Create(VagaViewModel vaga)
        {
            if (!ModelState.IsValid)
                return View(vaga);

            var response = await _httpClient.PostAsJsonAsync("/api/vaga", vaga);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Erro ao criar vaga via API");
            return View(vaga);
        }

        // GET: /Vaga/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var vaga = await _httpClient.GetFromJsonAsync<VagaViewModel>($"/api/vaga/{id}");
            if (vaga == null)
                return NotFound();

            return View(vaga);
        }

        // POST: /Vaga/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, VagaViewModel vaga)
        {
            if (id != vaga.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(vaga);

            var response = await _httpClient.PutAsJsonAsync($"/api/vaga/{id}", vaga);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Erro ao atualizar vaga via API");
            return View(vaga);
        }

        // GET: /Vaga/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var vaga = await _httpClient.GetFromJsonAsync<VagaViewModel>($"/api/vaga/{id}");
            if (vaga == null)
                return NotFound();

            return View(vaga);
        }

        // POST: /Vaga/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/vaga/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var vaga = await _httpClient.GetFromJsonAsync<VagaViewModel>($"/api/vaga/{id}");
            ModelState.AddModelError("", "Erro ao excluir vaga via API");
            return View("Delete", vaga);
        }
    }
}
