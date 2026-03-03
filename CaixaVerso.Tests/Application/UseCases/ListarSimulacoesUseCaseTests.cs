using Xunit;
using Moq;
using CaixaVerso.Application.UseCases;
using CaixaVerso.Domain.Interfaces;
using CaixaVerso.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

public class ListarSimulacoesUseCaseTests
{
    [Fact]
    public async Task Deve_Retornar_Simulacoes_Convertidas_Para_Response()
    {
        // Arrange
        var simulacoes = new List<Simulacao>
        {
            new Simulacao(
                1,
                1000.123m,
                1126.829m,
                12,
                "CDB Teste")
        };

        var repoMock = new Mock<ISimulacaoRepository>();
        repoMock.Setup(r => r.ListarPorCliente(1))
                .ReturnsAsync(simulacoes);

        var useCase = new ListarSimulacoesUseCase(repoMock.Object);

        // Act
        var resultado = await useCase.Executar(1);

        // Assert
        Assert.NotNull(resultado);
        Assert.Single(resultado);

        var item = resultado[0];

        Assert.Equal("CDB Teste", item.Produto);
        Assert.Equal(1000.12m, item.ValorInvestido); // arredondado
        Assert.Equal(1126.83m, item.ValorFinal);     // arredondado AwayFromZero
        Assert.Equal(12, item.PrazoMeses);
    }
}