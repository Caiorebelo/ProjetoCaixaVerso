using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ProjetoCaixaVerso.Models;

namespace ProjetoCaixaVerso.Infrastructure;

[ExcludeFromCodeCoverage]
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Produto> Produtos { get; set; } = null!;

}