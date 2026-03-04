using CaixaVerso.Application.DTOs;
using CaixaVerso.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CaixaVerso.Application.UseCases
{
    public class ObterSimulacaoPorIdUseCase
    {
        private readonly AppDbContext _db;

        public ObterSimulacaoPorIdUseCase(AppDbContext db)
        {
            _db = db;
        }

        public async Task<SimulacaoResultadoResponse?> Executar(int id)
        {
            var simulacao = await _db.Simulacoes
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(s => s.Id == id);

            if (simulacao == null)
                return null;

            return new SimulacaoResultadoResponse
            {
                Id = simulacao.Id,
                Produto = simulacao.ProdutoNome,
                ValorInicial = simulacao.ValorInvestido,
                ValorFinal = simulacao.ValorFinal,
                Rentabilidade = CalcularRentabilidade(simulacao.ValorInvestido, simulacao.ValorFinal),
                PrazoMeses = simulacao.PrazoMeses,     
                DataSimulacao = simulacao.DataSimulacao   
            };
        }

        private decimal CalcularRentabilidade(decimal valorInicial, decimal valorFinal)
        {
            if (valorInicial == 0) return 0;
            return (valorFinal - valorInicial) / valorInicial;
        }
    }
}
