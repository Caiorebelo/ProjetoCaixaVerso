using Microsoft.AspNetCore.Mvc;
using CaixaVerso.Application.UseCases;
using CaixaVerso.Application.DTOs;

namespace CaixaVerso.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SimulacoesController : ControllerBase
{
    private readonly CriarSimulacaoUseCase _useCase;
    private readonly ListarSimulacoesUseCase _listarUseCase;

    public SimulacoesController(
        CriarSimulacaoUseCase useCase,
        ListarSimulacoesUseCase listarUseCase)
    {
        _useCase = useCase;
        _listarUseCase = listarUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Criar(CriarSimulacaoRequest request)
    {
        var resultado = await _useCase.Executar(request);
        return Ok(resultado);
    }

    [HttpGet]
    public async Task<IActionResult> ListarPorCliente([FromQuery] int clienteId)
    {
        var resultado = await _listarUseCase.Executar(clienteId);
        return Ok(resultado);
    }
}