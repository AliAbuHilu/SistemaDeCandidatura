using System.Text;
using WebApp.Models;

namespace WebApp.Data.Txt
{
    public class CandidaturaDataAccess
    {
        private readonly string _caminho;

        public CandidaturaDataAccess(string caminho)
        {
            _caminho = caminho;
            if (!File.Exists(_caminho))
                File.WriteAllText(_caminho, "");
        }

        public List<Candidatura> ObterTodos()
        {
            return File.ReadAllLines(_caminho)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => FromCsv(l))
                .ToList();
        }

        public Candidatura? ObterPorId(int id)
        {
            return ObterTodos().FirstOrDefault(c => c.Id == id);
        }

        public void Adicionar(Candidatura candidatura)
        {
            candidatura.Id = ObterTodos().Any() ? ObterTodos().Max(c => c.Id) + 1 : 1;
            File.AppendAllText(_caminho, ToCsv(candidatura) + Environment.NewLine);
        }

        public void Atualizar(Candidatura candidatura)
        {
            var candidaturas = ObterTodos();
            var index = candidaturas.FindIndex(c => c.Id == candidatura.Id);
            if (index >= 0)
            {
                candidaturas[index] = candidatura;
                SalvarTodos(candidaturas);
            }
        }

        public void Excluir(int id)
        {
            var candidaturas = ObterTodos().Where(c => c.Id != id).ToList();
            SalvarTodos(candidaturas);
        }

        private void SalvarTodos(List<Candidatura> candidaturas)
        {
            File.WriteAllLines(_caminho, candidaturas.Select(c => ToCsv(c)));
        }

        private string ToCsv(Candidatura c) =>
            $"{c.Id};{c.PessoaId};{c.VagaId};{c.Aprovado}";

        private Candidatura FromCsv(string linha)
        {
            var partes = linha.Split(';');
            return new Candidatura
            {
                Id = int.Parse(partes[0]),
                PessoaId = int.Parse(partes[1]),
                VagaId = int.Parse(partes[2]),              
                Aprovado = bool.Parse(partes[4])
            };
        }
    }
}
