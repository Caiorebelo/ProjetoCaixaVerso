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

    public async Task<CriarSimulacaoResponse?> Executar(CriarSimulacaoRequest request)
    {
        // Validações básicas
        if (request.ClienteId <= 0 || request.Valor <= 0 || request.PrazoMeses <= 0 || string.IsNullOrEmpty(request.TipoProduto))
            return null; // Controller retorna 400

        // Buscar produto elegível
        var produto = await _produtoRepository.ObterPorTipo(request.TipoProduto);

        if (produto == null)
            return null; // Controller retorna 422

        // Calcular valor final
        var valorFinal = Math.Round(
            _calculadora.Calcular(request.Valor, produto.Rentabilidade, request.PrazoMeses),
            2,
            MidpointRounding.AwayFromZero
        );

        // Criar entidade de simulação
        var simulacao = new Simulacao(
            request.ClienteId,
            request.Valor,
            valorFinal,
            request.PrazoMeses,
            produto.Nome
        );

        await _simulacaoRepository.Adicionar(simulacao);

        // Retornar DTO no formato exigido
        return new CriarSimulacaoResponse
        {
            ProdutoValidado = new ProdutoResponse
            {
                Id = produto.Id,
                Nome = produto.Nome,
                TipoProduto = produto.Tipo,
                Rentabilidade = produto.Rentabilidade,
                Risco = produto.Risco
            },
            ResultadoSimulacao = new ResultadoSimulacao
            {
                ValorFinal = valorFinal,
                PrazoMeses = request.PrazoMeses
            },
            DataSimulacao = DateTime.UtcNow
        };
    }
}
