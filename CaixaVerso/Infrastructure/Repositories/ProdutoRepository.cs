using Microsoft.EntityFrameworkCore;
using CaixaVerso.Domain.Entities;
using CaixaVerso.Domain.Interfaces;
using CaixaVerso.Infrastructure.Data;

namespace CaixaVerso.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Produto?> ObterPorId(int id)
    {
        return await _context.Produtos
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Produto?> ObterPorTipo(string tipo)
    {
        return await _context.Produtos
            .FirstOrDefaultAsync(p => p.Tipo == tipo);
    }
}