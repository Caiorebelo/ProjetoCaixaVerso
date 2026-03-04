public class SimulacaoResultadoResponse
{
    public int Id { get; set; }
    public string Produto { get; set; } = string.Empty; 
    public decimal ValorInicial { get; set; }
    public decimal ValorFinal { get; set; }
    public decimal Rentabilidade { get; set; }
    public int PrazoMeses { get; set; }
    public DateTime DataSimulacao { get; set; }
}
