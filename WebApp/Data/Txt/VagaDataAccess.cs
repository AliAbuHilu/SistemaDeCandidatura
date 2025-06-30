using System.Text;
using WebApp.Models;

namespace WebApp.Data.Txt
{
    public class VagaDataAccess
    {
        private readonly string _caminho;

        public VagaDataAccess(string caminho)
        {
            _caminho = caminho;
            if (!File.Exists(_caminho))
                File.WriteAllText(_caminho, "");
        }

        public List<Vaga> ObterTodos()
        {
            var vagas = new List<Vaga>();

            foreach (var linha in File.ReadAllLines(_caminho))
            {
                if (string.IsNullOrWhiteSpace(linha)) continue;

                try
                {
                    var vaga = FromCsv(linha);
                    vagas.Add(vaga);
                }
                catch
                {
                    // Se a linha estiver mal formatada, apenas ignora
                    continue;
                }
            }

            return vagas;
        }

        public Vaga? ObterPorId(int id)
        {
            return ObterTodos().FirstOrDefault(v => v.Id == id);
        }

        public void Adicionar(Vaga vaga)
        {
            var vagas = ObterTodos();
            vaga.Id = vagas.Any() ? vagas.Max(p => p.Id) + 1 : 1;
            File.AppendAllText(_caminho, ToCsv(vaga) + Environment.NewLine);
        }

        public void Atualizar(Vaga vaga)
        {
            var vagas = ObterTodos();
            var index = vagas.FindIndex(p => p.Id == vaga.Id);
            if (index >= 0)
            {
                vagas[index] = vaga;
                SalvarTodos(vagas);
            }
        }

        public void Excluir(int id)
        {
            var vagas = ObterTodos().Where(p => p.Id != id).ToList();
            SalvarTodos(vagas);
        }

        private void SalvarTodos(List<Vaga> vagas)
        {
            File.WriteAllLines(_caminho, vagas.Select(v => ToCsv(v)));
        }

        private string ToCsv(Vaga v) =>
            $"{v.Id};{v.Titulo};{v.Descricao};{v.Ativa}";

        private Vaga FromCsv(string linha)
        {
            var partes = linha.Split(';');

            if (partes.Length < 4)
                throw new FormatException($"Linha inválida no arquivo de vagas: \"{linha}\"");

            return new Vaga
            {
                Id = int.Parse(partes[0]),
                Titulo = partes[1],
                Descricao = partes[2],
                Ativa = bool.Parse(partes[3])
            };
        }
    }
}
