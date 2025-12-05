using System.ComponentModel.DataAnnotations;

namespace MvcEcocycle.Models
{
    public class Colaboradores
    {
        public int ColaboradoresId { get; set; }

        [Required(ErrorMessage = "O nome do colaborador é obrigatório para dar continuação.")]
        [StringLength(100, ErrorMessage = "O nome do colaborador não deve ter mais do que 100 caractéres.")]
        public string? Nome { get; set; }

        [Display(Name = "Qtd. de Lixo (Kg)")]
        public int? Qtd { get; set; } = 0;
    }
}
