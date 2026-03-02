namespace CaixaVerso.Domain.Services;

public interface ICalculadoraInvestimento
{
    decimal Calcular(decimal valor,
                     decimal rentabilidadeAnual,
                     int prazoMeses);
}