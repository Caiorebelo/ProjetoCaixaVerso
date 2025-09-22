using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ProjetoCaixaVerso.Infrastructure;
using ProjetoCaixaVerso.Models;

public class ProdutoRepositoryTests
{
    private AppDbContext GetInMemoryContext()
    {
        // Cria uma conexão SQLite em memória
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open(); // precisa manter aberta

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connection)
            .Options;

        var context = new AppDbContext(options);

        // Garante que o schema (migrações) seja criado
        context.Database.EnsureCreated();

        return context;

    }

    [Fact]
    public void ObterProduto_DeveRetornarProduto()
    {
        // Arrange
        using var context = GetInMemoryContext();
        var repository = new ProdutoRepository(context);
        var produto = new Produto { Id = 1, Nome = "Emprestimo", TaxaJurosAnual = 5.0M, PrazoMaximoMeses = 12 };
        context.Produtos.Add(produto);

        // Act
        var salvo = repository.ObterProduto(1);


        // Assert
        Assert.NotNull(salvo);
        Assert.Equal("Emprestimo", salvo!.Nome);
        Assert.Equal(5.0M, salvo.TaxaJurosAnual);
        Assert.Equal(12, salvo.PrazoMaximoMeses);
    }

    [Fact]
    public void CriarProduto_DeveAdicionarProduto()
    {
        // Arrange
        using var context = GetInMemoryContext();
        var repository = new ProdutoRepository(context);

        // Act
        var criado = repository.CriarProduto("Emprestimo", 5.0M, 12);
        var salvo = context.Produtos.FirstOrDefault(p => p.Nome == "Emprestimo");

        // Assert
        Assert.True(criado);
        Assert.NotNull(salvo);
        Assert.Equal("Emprestimo", salvo!.Nome);
        Assert.Equal(5.0M, salvo.TaxaJurosAnual);
        Assert.Equal(12, salvo.PrazoMaximoMeses);
    }

    [Fact]
    public void DeletarProduto_DeveRemoverProduto()
    {
        // Arrange
        using var context = GetInMemoryContext();
        var repository = new ProdutoRepository(context);
        var produto = new Produto { Id = 1, Nome = "Emprestimo", TaxaJurosAnual = 5.0M, PrazoMaximoMeses = 12 };
        context.Produtos.Add(produto);
        context.SaveChanges();

        // Act
        var deletado = repository.DeletarProduto(1);
        var salvo = context.Produtos.FirstOrDefault(p => p.Id == 1);

        // Assert
        Assert.True(deletado);
        Assert.Null(salvo);
    }

    [Fact]
    public void AlterarProduto_DeveAtualizarProduto()
    {
        // Arrange
        using var context = GetInMemoryContext();
        var repository = new ProdutoRepository(context);
        var produto = new Produto { Id = 1, Nome = "Emprestimo", TaxaJurosAnual = 5.0M, PrazoMaximoMeses = 12 };
        context.Produtos.Add(produto);
        context.SaveChanges();

        // Act
        var alterado = repository.AlterarProduto(1, "NovoNome", 6.0M, 18);
        var salvo = context.Produtos.FirstOrDefault(p => p.Id == 1);

        // Assert
        Assert.True(alterado);
        Assert.NotNull(salvo);
        Assert.Equal("NovoNome", salvo!.Nome);
        Assert.Equal(6.0M, salvo.TaxaJurosAnual);
        Assert.Equal(18, salvo.PrazoMaximoMeses);
    }
    
}