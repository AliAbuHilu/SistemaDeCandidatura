using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class VagaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(500)]
        public string Descricao { get; set; }

        public bool Ativa { get; set; }
    }
}
