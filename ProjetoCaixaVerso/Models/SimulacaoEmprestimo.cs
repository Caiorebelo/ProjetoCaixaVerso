
namespace ProjetoCaixaVerso.Models;
public class SimulacaoEmprestimo
{
    public Produto Produto { get; set; }
    public decimal ValorSolicitado { get; set; }
    public int PrazoMeses { get; set; }
    public decimal TaxaJurosEfetivaMensal { get; set; }
    public decimal ValorTotalComJuros { get; set; }
    public decimal ParcelaMensal { get; set; }
    public List<MemoriaCalculo> MemoriaCalculo { get; set; }
}


public class MemoriaCalculo
{
    public int Mes { get; set; }
    public decimal SaldoDevedorInicial { get; set; }
    public decimal Juros { get; set; }
    public decimal Amortizacao { get; set; }
    public decimal SaldoDevedorFinal { get; set; }
}
