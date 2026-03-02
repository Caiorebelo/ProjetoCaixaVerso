using CaixaVerso.Domain.Interfaces;
using CaixaVerso.Application.DTOs;

namespace CaixaVerso.Application.UseCases;

public class ListarSimulacoesUseCase
{
    private readonly ISimulacaoRepository _repository;

    public ListarSimulacoesUseCase(ISimulacaoRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<SimulacaoResponse>> Executar(int clienteId)
    {
        var simulacoes = await _repository.ListarPorCliente(clienteId);

        return simulacoes.Select(s => new SimulacaoResponse
        {
            Produto = s.ProdutoNome,
            ValorInvestido = Math.Round(s.ValorInvestido, 2),
            ValorFinal = Math.Round(s.ValorFinal, 2, MidpointRounding.AwayFromZero),
            PrazoMeses = s.PrazoMeses,
            DataSimulacao = s.DataSimulacao
        }).ToList();
    }
}