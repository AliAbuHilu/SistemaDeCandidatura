namespace WebApp.DTOs
{
    public class CandidaturaDTO
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public int VagaId { get; set; }
        public bool Aprovado { get; set; }
    }
}
