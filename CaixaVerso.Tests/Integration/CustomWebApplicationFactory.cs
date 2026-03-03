using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CaixaVerso.Infrastructure.Data;
using CaixaVerso.Domain.Entities;

public class CustomWebApplicationFactory 
    : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            var scope = services.BuildServiceProvider().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // 🔥 GARANTE BANCO LIMPO
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Produtos.Add(new Produto
            {
                Nome = "CDB Banco XPTO",
                Tipo = "CDB",
                Rentabilidade = 0.01m,
                Risco = "Baixo",
                PrazoMinMeses = 1,
                PrazoMaxMeses = 60,
                ValorMin = 100,
                ValorMax = 100000
            });

            context.SaveChanges();
        });
    }
}