using System.Text.Json;
using WebApp.Models;

namespace WebApp.Services
{
    public class CandidaturaService
    {
        private readonly string _arquivo = "Data/candidaturas.txt";
        private List<Candidatura> _candidaturas = new();

        public CandidaturaService()
        {
            if (File.Exists(_arquivo))
            {
                var json = File.ReadAllText(_arquivo);
                if (!string.IsNullOrWhiteSpace(json))
                    _candidaturas = JsonSerializer.Deserialize<List<Candidatura>>(json) ?? new();
            }
        }

        private void Salvar() =>
            File.WriteAllText(_arquivo, JsonSerializer.Serialize(_candidaturas));

        public List<Candidatura> Listar() => _candidaturas;

        public List<Candidatura> ListarPorVaga(int vagaId) =>
            _candidaturas.Where(c => c.VagaId == vagaId).ToList();

        public void Atualizar(Candidatura atualizada)
        {
            var index = _candidaturas.FindIndex(c => c.Id == atualizada.Id);
            if (index >= 0)
            {
                _candidaturas[index] = atualizada;
                Salvar();
            }
        }

        public void Remover(int id)
        {
            var candidatura = _candidaturas.FirstOrDefault(c => c.Id == id);
            if (candidatura != null)
            {
                _candidaturas.Remove(candidatura);
                Salvar();
            }
        }






        public void Adicionar(Candidatura candidatura)
        {
            candidatura.Id = _candidaturas.Any() ? _candidaturas.Max(c => c.Id) + 1 : 1;
            _candidaturas.Add(candidatura);
            Salvar();
        }

        public void Aprovar(int id)
        {
            var candidatura = _candidaturas.FirstOrDefault(c => c.Id == id);
            if (candidatura != null)
            {
                candidatura.Aprovado = true;
                Salvar();
            }
        }
    }
}
