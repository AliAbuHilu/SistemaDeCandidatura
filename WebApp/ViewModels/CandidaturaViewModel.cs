namespace WebApp.ViewModels
{
    public class CandidaturaViewModel
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public int VagaId { get; set; }
        public bool Aprovado { get; set; }

        // Para exibição
        public string? PessoaNome { get; set; }
        public string? VagaTitulo { get; set; }
    }
}
