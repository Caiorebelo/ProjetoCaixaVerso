using ProjetoCaixaVerso.Infrastructure;
using ProjetoCaixaVerso.Models;

public interface IProdutoRepository
{
    bool AlterarProduto(int id, string? nome, decimal? taxaJurosAnual, int? prazoMaximoMeses);
    bool CriarProduto(string nome, decimal taxaJurosAnual, int prazoMaximoMeses);
    bool DeletarProduto(int id);
    Produto? ObterProduto(int id);
}

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public Produto? ObterProduto(int id)
    {
        return _context.Produtos.Find(id);
    }

    public bool DeletarProduto(int id)
    {
        var produto = _context.Produtos.Find(id);
        if (produto == null)
        {
            return false;
        }

        _context.Produtos.Remove(produto);
        _context.SaveChanges();
        return true;
    }

    public bool AlterarProduto(int id, string? nome, decimal? taxaJurosAnual, int? prazoMaximoMeses)
    {
        var produto = _context.Produtos.Find(id);
        if (produto == null)
        {
            return false;
        }

        if (nome != null)
        {
            produto.Nome = nome;
        }
        if (taxaJurosAnual.HasValue)
        {
            produto.TaxaJurosAnual = taxaJurosAnual.Value;
        }
        if (prazoMaximoMeses.HasValue)
        {
            produto.PrazoMaximoMeses = prazoMaximoMeses.Value;
        }

        _context.SaveChanges();
        return true;
    }

    public bool CriarProduto(string nome, decimal taxaJurosAnual, int prazoMaximoMeses)
    {
        var produto = new Produto
        {
            Nome = nome,
            TaxaJurosAnual = taxaJurosAnual,
            PrazoMaximoMeses = prazoMaximoMeses
        };

        _context.Produtos.Add(produto);
        _context.SaveChanges();
        return true;
    }
}