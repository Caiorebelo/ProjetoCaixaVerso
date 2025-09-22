using Moq;
using ProjetoCaixaVerso.Models;
using ProjetoCaixaVerso.Services;

namespace ProjetoCaixaVerso.Tests;

public class ProdutoServiceTests
{
    [Fact]
    public void ObterProduto_DeveRetornarProduto_QuandoExistir()
    {
        // Arrange
        var mockRepo = new Mock<IProdutoRepository>();
        mockRepo.Setup(r => r.ObterProduto(1))
                .Returns(new Produto { Id = 1, Nome = "Produto Teste", TaxaJurosAnual = 18.0M, PrazoMaximoMeses = 24 });

        var service = new ProdutoService(mockRepo.Object);


        // Act
        var resultado = service.ObterProduto(1);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Produto Teste", resultado.Nome);
        Assert.Equal(18.0m, resultado.TaxaJurosAnual);
        Assert.Equal(24, resultado.PrazoMaximoMeses);
    }

    [Fact]
    public void CriarProduto_DeveRetornarTrue_QuandoCriarComSucesso()
    {
        // Arrange
        var mockRepo = new Mock<IProdutoRepository>();
        mockRepo.Setup(r => r.CriarProduto("Novo Produto", 15.0M, 12)).Returns(true);

        var service = new ProdutoService(mockRepo.Object);

        // Act
        var resultado = service.CriarProduto("Novo Produto", 15.0M, 12);

        // Assert
        Assert.True(resultado);
    }

    [Fact]
    public void DeletarProduto_DeveRetornarTrue_QuandoDeletarComSucesso()
    {
        // Arrange
        var mockRepo = new Mock<IProdutoRepository>();
        mockRepo.Setup(r => r.DeletarProduto(1)).Returns(true);

        var service = new ProdutoService(mockRepo.Object);

        // Act
        var resultado = service.DeletarProduto(1);

        // Assert
        Assert.True(resultado);
    }

    [Fact]
    public void AlterarProduto_DeveRetornarTrue_QuandoAlterarComSucesso()
    {
        // Arrange
        var mockRepo = new Mock<IProdutoRepository>();
        mockRepo.Setup(r => r.AlterarProduto(1, "Produto Alterado", 20.0M, 18)).Returns(true);

        var service = new ProdutoService(mockRepo.Object);

        // Act
        var resultado = service.AlterarProduto(1, "Produto Alterado", 20.0M, 18);

        // Assert
        Assert.True(resultado);
    }
}
