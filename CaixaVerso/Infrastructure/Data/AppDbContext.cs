using Microsoft.EntityFrameworkCore;
using CaixaVerso.Domain.Entities;

namespace CaixaVerso.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Simulacao> Simulacoes { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.Entity<Produto>().HasData(
            new Produto
            {
                Id = 1,
                Nome = "CDB Caixa 2026",
                Tipo = "CDB",
                Rentabilidade = 0.12m,
                Risco = "Baixo"
            },
            new Produto
            {
                Id = 2,
                Nome = "LCI Caixa 2027",
                Tipo = "LCI",
                Rentabilidade = 0.10m,
                Risco = "Baixo"
            }
        );
}