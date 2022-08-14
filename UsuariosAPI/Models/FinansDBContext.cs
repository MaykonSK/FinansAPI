using Microsoft.EntityFrameworkCore;

namespace UsuariosAPI.Models
{
    public class FinansDBContext : DbContext
    {
        public FinansDBContext(DbContextOptions<FinansDBContext> options) : base(options)
        {

        }

        public virtual DbSet<UsuarioFinans> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioFinans>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("Id");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
