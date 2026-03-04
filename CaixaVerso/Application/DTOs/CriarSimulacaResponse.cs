public class CriarSimulacaoResponse
{
    public ProdutoResponse ProdutoValidado { get; set; } = new();
    public ResultadoSimulacao ResultadoSimulacao { get; set; } = new();
    public DateTime DataSimulacao { get; set; }
}

public class ProdutoResponse
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string TipoProduto { get; set; } = string.Empty;
    public decimal Rentabilidade { get; set; }
    public string Risco { get; set; } = string.Empty;
}

public class ResultadoSimulacao
{
    public decimal ValorFinal { get; set; }
    public int PrazoMeses { get; set; }
}
