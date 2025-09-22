using Moq;
using Microsoft.AspNetCore.Mvc;
using ProjetoCaixaVerso.Controllers;
using ProjetoCaixaVerso.Services;
using ProjetoCaixaVerso.Models;

public class EmprestimoControllerTests
{
    [Fact]
    public void Simular_DeveRetornarBadRequest_QuandoParametrosSaoInvalidos()
    {
        // Arrange
        var mockService = new Mock<IEmprestimoService>();
        mockService.Setup(s => s.SimularEmprestimo(It.IsAny<int>(), It.IsAny<decimal>(), It.IsAny<int>()))
                   .Returns((SimulacaoEmprestimo?)null);

        var controller = new EmprestimoController(mockService.Object);

        // Act
        var result = controller.Simular(1, 10000M, 12).Result;

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Não foi possível simular o empréstimo com os parâmetros fornecidos.", badRequestResult.Value);
    }

    [Fact]
    public void Simular_DeveRetornarOk_QuandoParametrosSaoValidos()
    {
        // Arrange
        var mockService = new Mock<IEmprestimoService>();
        var simulacao = new SimulacaoEmprestimo
        {
            Produto = new Produto { Id = 1, Nome = "Produto Teste", TaxaJurosAnual = 12.0M, PrazoMaximoMeses = 24 },
            ValorSolicitado = 10000M,
            PrazoMeses = 12,
            MemoriaCalculo = new List<MemoriaCalculo>
            {
                new MemoriaCalculo { Mes = 1, Amortizacao = 833.33M, Juros = 100.00M, SaldoDevedorInicial = 10000M, SaldoDevedorFinal = 9166.67M }
            }
        };
        mockService.Setup(s => s.SimularEmprestimo(1, 10000M, 12)).Returns(simulacao);

        var controller = new EmprestimoController(mockService.Object);

        // Act
        var result = controller.Simular(1, 10000M, 12).Result;

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var retornoSimulacao = Assert.IsType<SimulacaoEmprestimo>(okResult.Value);
        Assert.Equal(1, retornoSimulacao.Produto.Id);
        Assert.Equal(10000M, retornoSimulacao.ValorSolicitado);
        Assert.Equal(12, retornoSimulacao.PrazoMeses);
    }
}