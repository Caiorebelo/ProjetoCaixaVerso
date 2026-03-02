using Xunit;
using Moq;
using CaixaVerso.Application.UseCases;
using CaixaVerso.Domain.Interfaces;
using CaixaVerso.Domain.Entities;
using CaixaVerso.Domain.Services;
using CaixaVerso.Application.DTOs;

public class CriarSimulacaoUseCaseTests
{
    [Fact]
    public async Task Deve_Calcular_ValorFinal_Corretamente()
    {
        // Arrange
        var produto = new Produto(1, "CDB Teste", "CDB", 0.12m, "Baixo");

        var produtoRepoMock = new Mock<IProdutoRepository>();
        produtoRepoMock.Setup(x => x.ObterPorTipo("CDB"))
            .ReturnsAsync(produto);

        var simulacaoRepoMock = new Mock<ISimulacaoRepository>();

        var calculadora = new CalculadoraInvestimento();

        var useCase = new CriarSimulacaoUseCase(
            produtoRepoMock.Object,
            simulacaoRepoMock.Object,
            calculadora);

        var request = new CriarSimulacaoRequest
        {
            ClienteId = 1,
            Valor = 1000,
            PrazoMeses = 12,
            TipoProduto = "CDB"
        };

        // Act
        var resultado = await useCase.Executar(request);

        // Assert
        Assert.True(resultado.ValorFinal > 1000);

    }

    [Fact]
    public async Task Deve_Lancar_Excecao_Quando_Produto_Nao_Encontrado()
    {
        var produtoRepoMock = new Mock<IProdutoRepository>();
        produtoRepoMock.Setup(x => x.ObterPorTipo("CDB"))
            .ReturnsAsync((Produto?)null);

        var simulacaoRepoMock = new Mock<ISimulacaoRepository>();

        var calculadora = new CalculadoraInvestimento();

        var useCase = new CriarSimulacaoUseCase(
            produtoRepoMock.Object,
            simulacaoRepoMock.Object,
            calculadora);

        var request = new CriarSimulacaoRequest
        {
            ClienteId = 1,
            Valor = 1000,
            PrazoMeses = 12,
            TipoProduto = "CDB"
        };

        await Assert.ThrowsAsync<ArgumentException>(
            () => useCase.Executar(request)
        );
    }
}