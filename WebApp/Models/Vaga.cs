using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Vaga
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(500)]
        public string Descricao { get; set; }

        public bool Ativa { get; set; } = true;
    }
}
