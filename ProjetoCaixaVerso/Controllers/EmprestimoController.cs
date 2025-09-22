using Microsoft.AspNetCore.Mvc;
using ProjetoCaixaVerso.Services;

namespace ProjetoCaixaVerso.Controllers;

[ApiController]
[Route("emprestimo")]
public class EmprestimoController : ControllerBase
{
    private readonly IEmprestimoService _emprestimoService;
    public EmprestimoController(IEmprestimoService emprestimoService)
    {
        _emprestimoService = emprestimoService;
    }


    [HttpPost("simular")]
    public Task<IActionResult> Simular(int produtoId, decimal valorSolicitado, int prazoMeses)
    {
        var simulacao = _emprestimoService.SimularEmprestimo(produtoId, valorSolicitado, prazoMeses);
        if (simulacao == null)
        {
            return Task.FromResult<IActionResult>(NotFound());
        }
        return Task.FromResult<IActionResult>(Ok(simulacao));
    }
}