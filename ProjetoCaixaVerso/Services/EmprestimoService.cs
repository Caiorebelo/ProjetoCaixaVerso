
using ProjetoCaixaVerso.Models;

namespace ProjetoCaixaVerso.Services;

public interface IEmprestimoService
{
    SimulacaoEmprestimo? SimularEmprestimo(int produtoId, decimal valorSolicitado, int prazoMeses);
}

public class EmprestimoService : IEmprestimoService
{
    private readonly IProdutoRepository _produtoRepository;
    public EmprestimoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }
    public SimulacaoEmprestimo? SimularEmprestimo(int produtoId, decimal valorSolicitado, int prazoMeses)
    {
        var produto = _produtoRepository.ObterProduto(produtoId);
        if (produto == null || prazoMeses > produto.PrazoMaximoMeses)
        {
            return null;
        }
        var simulacaoProduto = new SimulacaoEmprestimo();
        simulacaoProduto.Produto = produto;

        simulacaoProduto.ValorSolicitado = valorSolicitado;
        simulacaoProduto.PrazoMeses = prazoMeses;
        simulacaoProduto.TaxaJurosEfetivaMensal = Math.Round((decimal)Math.Pow(1 + (double)simulacaoProduto.Produto.TaxaJurosAnual / 100.0, 1.0 / 12.0) - 1, 6);

        simulacaoProduto.ParcelaMensal = valorSolicitado * simulacaoProduto.TaxaJurosEfetivaMensal /
            (1 - (decimal)Math.Pow((double)(1 + simulacaoProduto.TaxaJurosEfetivaMensal), -1 * simulacaoProduto.PrazoMeses));
        simulacaoProduto.ValorTotalComJuros = simulacaoProduto.ValorSolicitado * (decimal)Math.Pow((double)(1 + simulacaoProduto.TaxaJurosEfetivaMensal), simulacaoProduto.PrazoMeses);
        simulacaoProduto.MemoriaCalculo = new List<MemoriaCalculo>();

        for (int i = 1; i <= simulacaoProduto.PrazoMeses; i++)
        {
            var memoriaCalculo = new MemoriaCalculo();
            memoriaCalculo.Mes = i;
            if (i == 1)
            {
                memoriaCalculo.SaldoDevedorInicial = simulacaoProduto.ValorSolicitado;
            }
            else
            {
                memoriaCalculo.SaldoDevedorInicial = simulacaoProduto.MemoriaCalculo[i - 2].SaldoDevedorFinal;
            }
            memoriaCalculo.Juros = memoriaCalculo.SaldoDevedorInicial * simulacaoProduto.TaxaJurosEfetivaMensal;
            memoriaCalculo.Amortizacao = simulacaoProduto.ParcelaMensal - memoriaCalculo.Juros;
            memoriaCalculo.SaldoDevedorFinal = memoriaCalculo.SaldoDevedorInicial - memoriaCalculo.Amortizacao;
            simulacaoProduto.MemoriaCalculo.Add(memoriaCalculo);
        }
        return simulacaoProduto;
    }
}
