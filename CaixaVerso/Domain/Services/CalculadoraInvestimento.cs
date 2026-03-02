namespace CaixaVerso.Domain.Services;

public class CalculadoraInvestimento : ICalculadoraInvestimento
{
    public decimal Calcular(decimal valor,
                            decimal rentabilidadeAnual,
                            int prazoMeses)
    {
        decimal taxaMensal = rentabilidadeAnual / 12;

        decimal fator = (decimal)Math.Pow(
            (double)(1 + taxaMensal),
            prazoMeses);

        return valor * fator;
    }
}