namespace CaixaVerso.Domain.Entities;

public class Simulacao
{
    public int Id { get; protected set; }

    public int ClienteId { get; private set; }

    public decimal ValorInvestido { get; private set; }

    public decimal ValorFinal { get; private set; }

    public int PrazoMeses { get; private set; }

    public string ProdutoNome { get; private set; }

    public DateTime DataSimulacao { get; private set; }

    public Simulacao(int clienteId,
                     decimal valorInvestido,
                     decimal valorFinal,
                     int prazoMeses,
                     string produtoNome)
    {
        ClienteId = clienteId;
        ValorInvestido = valorInvestido;
        ValorFinal = valorFinal;
        PrazoMeses = prazoMeses;
        ProdutoNome = produtoNome;
        DataSimulacao = DateTime.UtcNow;
    }
}