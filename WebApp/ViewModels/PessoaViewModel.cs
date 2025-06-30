using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class PessoaViewModel
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
