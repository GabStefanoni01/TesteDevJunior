using Microsoft.EntityFrameworkCore;
using MinhaApiComSQLite.Models;

namespace MinhaApiComSQLite.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos => Set<Produto>();
        public DbSet<Categoria> Categorias => Set<Categoria>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasIndex(c => c.Nome).IsUnique();
                entity.Property(c => c.Nome).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasIndex(p => p.Nome).IsUnique();
                entity.Property(p => p.Nome).HasMaxLength(150).IsRequired();
                entity.Property(p => p.Preco).HasColumnType("decimal(18,2)").IsRequired();

                entity.HasOne(p => p.Categoria)
                    .WithMany(c => c.Produtos)
                    .HasForeignKey(p => p.CategoriaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
