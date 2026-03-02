using CaixaVerso.Domain.Entities;
using CaixaVerso.Domain.Interfaces;
using CaixaVerso.Domain.Services;
using CaixaVerso.Application.DTOs;

namespace CaixaVerso.Application.UseCases;

public class CriarSimulacaoUseCase
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly ISimulacaoRepository _simulacaoRepository;
    private readonly ICalculadoraInvestimento _calculadora;

    public CriarSimulacaoUseCase(
        IProdutoRepository produtoRepository,
        ISimulacaoRepository simulacaoRepository,
        ICalculadoraInvestimento calculadora)
    {
        _produtoRepository = produtoRepository;
        _simulacaoRepository = simulacaoRepository;
        _calculadora = calculadora;
    }

    public async Task<SimulacaoResultadoResponse> Executar(CriarSimulacaoRequest request)
    {
        // Validações
        if (request.Valor <= 0)
            throw new ArgumentException("Valor investido deve ser maior que zero.");

        if (request.PrazoMeses <= 0)
            throw new ArgumentException("Prazo deve ser maior que zero.");

        // Buscar produto
        var produto = await _produtoRepository.ObterPorTipo(request.TipoProduto);

        if (produto == null)
            throw new ArgumentException("Produto não encontrado.");

        // Calculo
        var valorCalculado = _calculadora.Calcular(
            request.Valor,
            produto.Rentabilidade,
            request.PrazoMeses
        );

        // Arredondamento
        var valorFinal = Math.Round(
            valorCalculado,
            2,
            MidpointRounding.AwayFromZero
        );

        // Criar entidade
        var simulacao = new Simulacao(
            request.ClienteId,
            request.Valor,
            valorFinal,
            request.PrazoMeses,
            produto.Nome
        );

        // Persistir
        await _simulacaoRepository.Adicionar(simulacao);

        // Retornar DTO
        return new SimulacaoResultadoResponse
        {
            Produto = produto.Nome,
            ValorFinal = valorFinal
        };
    }
}