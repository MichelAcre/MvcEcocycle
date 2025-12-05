using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MvcEcocycle.Models
{
    public class Movimentacoes
    {
        public int MovimentacoesId { get; set; }

        public string? ColaboradoresId { get; set; }
        public Colaboradores? Colaboradores { get; set; }

        [Required(ErrorMessage = "O campo da quantidade que vai ser movimentada é obrigatório.")]
        [StringLength(100, ErrorMessage = "A quantidade não pode ter mais de 100 caractéres")]
        [Display(Name = "Qtd. Movimentada")]
        public int Qtdmovimentada { get; set; }

        public DateTime Data { get; set; }

        [Display(Name = "Usuário")]
        public int? UsuarioId { get; set; }
        public IdentityUser? Usuario { get; set; }
    }
}
