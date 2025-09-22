
using ProjetoCaixaVerso.Models;

namespace ProjetoCaixaVerso.Services;

public interface IProdutoService
{
    bool AlterarProduto(int id, string? nome, decimal? taxaJurosAnual, int? prazoMaximoMeses);
    bool CriarProduto(string nome, decimal taxaJurosAnual, int prazoMaximoMeses);
    bool DeletarProduto(int id);
    Produto? ObterProduto(int id);
}

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }
    public bool CriarProduto(string nome, decimal taxaJurosAnual, int prazoMaximoMeses)
    {
        return _produtoRepository.CriarProduto(nome, taxaJurosAnual, prazoMaximoMeses);
    }

    public Produto? ObterProduto(int id)
    {
        return _produtoRepository.ObterProduto(id);
    }

    public bool DeletarProduto(int id)
    {
        return _produtoRepository.DeletarProduto(id);
    }

    public bool AlterarProduto(int id, string? nome, decimal? taxaJurosAnual, int? prazoMaximoMeses)
    {
        return _produtoRepository.AlterarProduto(id, nome, taxaJurosAnual, prazoMaximoMeses);
    }

}