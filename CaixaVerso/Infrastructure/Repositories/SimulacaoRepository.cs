using CaixaVerso.Domain.Entities;
using CaixaVerso.Domain.Interfaces;
using CaixaVerso.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CaixaVerso.Infrastructure.Repositories;

public class SimulacaoRepository : ISimulacaoRepository
{
    private readonly AppDbContext _context;

    public SimulacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Adicionar(Simulacao simulacao)
    {
        await _context.Simulacoes.AddAsync(simulacao);
        await _context.SaveChangesAsync();
    }


    public async Task<List<Simulacao>> ListarPorCliente(int clienteId)
    {
        return await _context.Simulacoes
            .Where(s => s.ClienteId == clienteId)
            .OrderByDescending(s => s.DataSimulacao)
            .ToListAsync();
    }
}