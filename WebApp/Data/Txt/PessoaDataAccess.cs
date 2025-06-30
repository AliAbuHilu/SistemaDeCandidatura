using System.Text;
using WebApp.Models;

namespace WebApp.Data.Txt
{
    public class PessoaDataAccess
    {
        private readonly string _caminho;

        public PessoaDataAccess(string caminho)
        {
            _caminho = caminho;

            if (!File.Exists(_caminho))
                File.WriteAllText(_caminho, "");
        }

        public List<Pessoa> ObterTodos()
        {
            return File.ReadAllLines(_caminho)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => FromCsv(l))
                .ToList();
        }

        public Pessoa? ObterPorId(int id)
        {
            return ObterTodos().FirstOrDefault(p => p.Id == id);
        }

        public void Adicionar(Pessoa pessoa)
        {
            var pessoas = ObterTodos();
            pessoa.Id = pessoas.Any() ? pessoas.Max(p => p.Id) + 1 : 1;

            File.AppendAllText(_caminho, ToCsv(pessoa) + Environment.NewLine);
        }

        public void Atualizar(Pessoa pessoa)
        {
            var pessoas = ObterTodos();
            var index = pessoas.FindIndex(p => p.Id == pessoa.Id);
            if (index >= 0)
            {
                pessoas[index] = pessoa;
                SalvarTodos(pessoas);
            }
        }

        public void Excluir(int id)
        {
            var pessoas = ObterTodos().Where(p => p.Id != id).ToList();
            SalvarTodos(pessoas);
        }

        private void SalvarTodos(List<Pessoa> pessoas)
        {
            File.WriteAllLines(_caminho, pessoas.Select(p => ToCsv(p)));
        }

        private string ToCsv(Pessoa p) =>
            $"{p.Id};{p.Nome};{p.Email};{p.Telefone}";

        private Pessoa FromCsv(string linha)
        {
            var partes = linha.Split(';');

            return new Pessoa
            {
                Id = int.Parse(partes[0]),
                Nome = partes[1],
                Email = partes[2],
                Telefone = partes[3]
            };
        }
    }
}
