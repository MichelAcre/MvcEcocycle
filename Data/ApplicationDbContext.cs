using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcEcocycle.Models;

namespace MvcEcocycle.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MvcEcocycle.Models.Movimentacoes> Movimentacoes { get; set; } = default!;
        public DbSet<MvcEcocycle.Models.Colaboradores> Colaboradores { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Chame a implementação base para IdentityDbContext funcionar
            base.OnModelCreating(modelBuilder);

            // 2. Configure a entidade Colaboradores
            modelBuilder.Entity<Colaboradores>()
                // 3. Selecione a propriedade Qtd
                .Property(c => c.Qtd)
                // 4. Defina o valor padrão como 0 (para int) ou 0.0 (para double)
                .HasDefaultValue(0); // Use 0.0 se Qtd for double
        }
    }
}
