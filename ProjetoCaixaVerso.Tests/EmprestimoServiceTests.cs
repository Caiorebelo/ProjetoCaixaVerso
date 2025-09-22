using Moq;
using ProjetoCaixaVerso.Models;
using ProjetoCaixaVerso.Services;

namespace ProjetoCaixaVerso.Tests;

public class EmprestimoServiceTests
{
    [Fact]
    public void SimularEmprestimo_DeveRetornarSimulacao_QuandoParametrosSaoValidos()
    {
        // Arrange
        var mockRepo = new Mock<IProdutoRepository>();
        mockRepo.Setup(r => r.ObterProduto(1))
                .Returns(new Produto { Id = 1, Nome = "Produto Teste", TaxaJurosAnual = 12.0M, PrazoMaximoMeses = 24 });

        var service = new EmprestimoService(mockRepo.Object);

        // Act
        var resultado = service.SimularEmprestimo(1, 10000M, 12);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(1, resultado.Produto.Id);
        Assert.Equal(10000M, resultado.ValorSolicitado);
        Assert.Equal(12, resultado.PrazoMeses);
        Assert.Equal(12, resultado.MemoriaCalculo.Count);

        var primeiroMes = resultado.MemoriaCalculo[0];
        Assert.Equal(1, primeiroMes.Mes);
        Assert.Equal(833.33M, Math.Round(primeiroMes.Amortizacao, 2));
        Assert.Equal(100.00M, Math.Round(primeiroMes.Juros, 2));
        Assert.Equal(933.33M, Math.Round(primeiroMes.SaldoDevedorInicial, 2));
        Assert.Equal(9166.67M, Math.Round(primeiroMes.SaldoDevedorFinal, 2));
    }
}