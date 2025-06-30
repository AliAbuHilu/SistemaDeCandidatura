using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Pessoa
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Telefone { get; set; }
    }
}
