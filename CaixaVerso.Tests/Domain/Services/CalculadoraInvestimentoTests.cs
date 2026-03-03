using Xunit;
using CaixaVerso.Domain.Services;

public class CalculadoraInvestimentoTests
{
    [Fact]
    public void Deve_Calcular_Valor_Final_Corretamente()
    {
        var calculadora = new CalculadoraInvestimento();

        var resultado = calculadora.Calcular(10000, 0.12m, 12);

        Assert.True(resultado > 10000);
    }
}