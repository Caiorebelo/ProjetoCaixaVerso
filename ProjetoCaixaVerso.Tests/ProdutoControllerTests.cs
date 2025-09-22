using Moq;
using Microsoft.AspNetCore.Mvc;
using ProjetoCaixaVerso.Controllers;
using ProjetoCaixaVerso.Services;

public class ProdutoControllerTests
{
    [Fact]
    public void Criar_DeveRetornarOk_QuandoCriar()
    {
        // Arrange
        var mockService = new Mock<IProdutoService>();

        var controller = new ProdutoController(mockService.Object);

        // Act
        var result = controller.Criar("Caixa", 5.0M, 12).Result;

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void Obter_DeveRetornarNotFound_QuandoProdutoNaoExiste()
    {
        // Arrange
        var mockService = new Mock<IProdutoService>();
        mockService.Setup(s => s.ObterProduto(It.IsAny<int>())).Returns((ProjetoCaixaVerso.Models.Produto?)null);
        var controller = new ProdutoController(mockService.Object);

        // Act
        var result = controller.Obter(1).Result;

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Produto nao encontrado.", notFoundResult.Value);
    }

    [Fact]
    public void Obter_DeveRetornarOk_QuandoProdutoExiste()
    {
        // Arrange
        var mockService = new Mock<IProdutoService>();
        mockService.Setup(s => s.ObterProduto(It.IsAny<int>())).Returns(new ProjetoCaixaVerso.Models.Produto { Id = 1, Nome = "Caixa", TaxaJurosAnual = 5.0M, PrazoMaximoMeses = 12 });
        var controller = new ProdutoController(mockService.Object);

        // Act
        var result = controller.Obter(1).Result;

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var produto = Assert.IsType<ProjetoCaixaVerso.Models.Produto>(okResult.Value);
        Assert.Equal(1, produto.Id);
        Assert.Equal("Caixa", produto.Nome);
    }

    [Fact]
    public void Deletar_DeveRetornarNotFound_QuandoProdutoNaoExiste()
    {
        // Arrange
        var mockService = new Mock<IProdutoService>();
        mockService.Setup(s => s.DeletarProduto(It.IsAny<int>())).Returns(false);
        var controller = new ProdutoController(mockService.Object);

        // Act
        var result = controller.Deletar(1).Result;

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Deletar_DeveRetornarOk_QuandoProdutoExiste()
    {
        // Arrange
        var mockService = new Mock<IProdutoService>();
        mockService.Setup(s => s.DeletarProduto(It.IsAny<int>())).Returns(true);
        var controller = new ProdutoController(mockService.Object);

        // Act
        var result = controller.Deletar(1).Result;

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void Alterar_DeveRetornarNotFound_QuandoProdutoNaoExiste()
    {
        // Arrange
        var mockService = new Mock<IProdutoService>();
        mockService.Setup(s => s.AlterarProduto(It.IsAny<int>(), It.IsAny<string?>(), It.IsAny<decimal?>(), It.IsAny<int?>())).Returns(false);
        var controller = new ProdutoController(mockService.Object);

        // Act
        var result = controller.Alterar(1, "NovoNome", 6.0M, 18).Result;

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Alterar_DeveRetornarOk_QuandoProdutoExiste()
    {
        // Arrange
        var mockService = new Mock<IProdutoService>();
        mockService.Setup(s => s.AlterarProduto(It.IsAny<int>(), It.IsAny<string?>(), It.IsAny<decimal?>(), It.IsAny<int?>())).Returns(true);
        var controller = new ProdutoController(mockService.Object);

        // Act
        var result = controller.Alterar(1, "NovoNome", 6.0M, 18).Result;

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
    }
}