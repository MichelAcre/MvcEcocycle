using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MvcEcocycle.Models
{
    public class Movimentacoes
    {
        public int MovimentacoesId { get; set; }
        [Display(Name = "Colaborador")]
        public int? ColaboradoresId { get; set; }
        public Colaboradores? Colaboradores { get; set; }

        [Required(ErrorMessage = "O campo da quantidade que vai ser movimentada é obrigatório.")]
        [Display(Name = "Qtd. Movimentada")]
        public int Qtdmovimentada { get; set; }

        public DateTime Data { get; set; }

        [Display(Name = "Usuário")]
        public string? UsuarioId { get; set; }
        public IdentityUser? Usuario { get; set; }
    }
}
