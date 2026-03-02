namespace CaixaVerso.Application.DTOs;

public class SimulacaoResponse
{
    public string Produto { get; set; } = string.Empty;
    public decimal ValorInvestido { get; set; }
    public decimal ValorFinal { get; set; }
    public int PrazoMeses { get; set; }
    public DateTime DataSimulacao { get; set; }
}