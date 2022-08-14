using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Finans.Models
{
    public partial class FinansContext : DbContext
    {
        public FinansContext()
        {
        }

        public FinansContext(DbContextOptions<FinansContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContasPagar> ContasPagars { get; set; }
        public virtual DbSet<ContasReceber> ContasRecebers { get; set; }
        public virtual DbSet<Endereco> Enderecos { get; set; }
        public virtual DbSet<Imovei> Imoveis { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Veiculo> Veiculos { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=localhost;Database=Finans;Trusted_Connection=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<ContasPagar>(entity =>
            {
                entity.ToTable("ContasPagar");

                entity.Property(e => e.Descricao).HasMaxLength(50);

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

                entity.Property(e => e.Valor).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Vencimento).HasColumnType("date");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.ContasPagars)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_ContasPagar_Usuario");
            });

            modelBuilder.Entity<ContasReceber>(entity =>
            {
                entity.ToTable("ContasReceber");

                entity.Property(e => e.Descricao).HasMaxLength(50);

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

                entity.Property(e => e.Valor).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Vencimento).HasColumnType("date");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.ContasRecebers)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_ContasReceber_Usuario");
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.Property(e => e.Bairro)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Cep)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsFixedLength(true);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.Municipio)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Rua)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Imovei>(entity =>
            {
                entity.Property(e => e.CodigoIptu)
                    .HasMaxLength(50)
                    .HasColumnName("CodigoIPTU");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EnderecoId).HasColumnName("EnderecoID");

                entity.Property(e => e.SitePrefeitura).HasMaxLength(100);

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

                entity.HasOne(d => d.Endereco)
                    .WithMany(p => p.Imoveis)
                    .HasForeignKey(d => d.EnderecoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Imoveis_Enderecos");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Imoveis)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_Imoveis_Usuario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.UsuarioId)
                    .ValueGeneratedNever()
                    .HasColumnName("UsuarioID");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Veiculo>(entity =>
            {
                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Modelo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Placa)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Renavam).HasMaxLength(50);

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Veiculos)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_Veiculos_Usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
