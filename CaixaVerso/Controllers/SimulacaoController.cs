using Microsoft.AspNetCore.Mvc;
using CaixaVerso.Application.UseCases;
using CaixaVerso.Application.DTOs;
using Swashbuckle.AspNetCore.Filters; // ✅ Necessário para os exemplos
using CaixaVerso.Documentation;      // ✅ Ajuste conforme o namespace da sua classe de exemplo

namespace CaixaVerso.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")] // ✅ Indica que a API sempre retorna JSON
public class SimulacoesController : ControllerBase
{
    private readonly CriarSimulacaoUseCase _useCase;
    private readonly ListarSimulacoesUseCase _listarUseCase;
    private readonly ObterSimulacaoPorIdUseCase _obterPorIdUseCase;

    public SimulacoesController(
        CriarSimulacaoUseCase useCase,
        ListarSimulacoesUseCase listarUseCase,
        ObterSimulacaoPorIdUseCase obterPorIdUseCase)
    {
        _useCase = useCase;
        _listarUseCase = listarUseCase;
        _obterPorIdUseCase = obterPorIdUseCase;
    }

    /// <summary>
    /// Cria uma nova simulação de investimento para um cliente.
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// POST /api/Simulacoes
    /// </remarks>
    /// <param name="request">Dados necessários para o cálculo da simulação.</param>
    /// <returns>Retorna os detalhes da simulação calculada.</returns>
    [HttpPost]
    [SwaggerRequestExample(typeof(CriarSimulacaoRequest), typeof(CriarSimulacaoRequestExample))] // ✅ Vincula o exemplo
    [ProducesResponseType(typeof(CriarSimulacaoResponse), StatusCodes.Status200OK)] // ✅ Documenta o sucesso
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // ✅ Documenta erro de validação
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)] // ✅ Documenta erro de negócio
    public async Task<IActionResult> Criar([FromBody] CriarSimulacaoRequest request)
    {
        // Validações básicas
        if (request.ClienteId <= 0 || request.Valor <= 0 || request.PrazoMeses <= 0 || string.IsNullOrEmpty(request.TipoProduto))
            return BadRequest("Campos obrigatórios inválidos");

        var resultado = await _useCase.Executar(request);

        // Produto elegível
        if (resultado == null)
            return UnprocessableEntity("Nenhum produto elegível encontrado");

        return Ok(resultado);
    }

    /// <summary>
    /// Lista todas as simulações realizadas por um cliente específico.
    /// </summary>
    /// <param name="clienteId">ID único do cliente.</param>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CriarSimulacaoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarPorCliente([FromQuery] int clienteId)
    {
        var resultado = await _listarUseCase.Executar(clienteId);
        return Ok(resultado);
    }

    /// <summary>
    /// Obtém os detalhes de uma simulação específica através do seu ID.
    /// </summary>
    /// <param name="id">ID da simulação gravada no banco.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CriarSimulacaoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var simulacao = await _obterPorIdUseCase.Executar(id);

        if (simulacao == null)
            return NotFound();

        return Ok(simulacao);
    }
}