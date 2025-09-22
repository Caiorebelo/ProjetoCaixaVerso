using Microsoft.AspNetCore.Mvc;
using ProjetoCaixaVerso.Services;

namespace ProjetoCaixaVerso.Controllers;

[ApiController]
[Route("produto")]
public class ProdutoController : ControllerBase
{
    
    private readonly IProdutoService _emprestimoService;
    public ProdutoController(IProdutoService emprestimoService)
    {
        _emprestimoService = emprestimoService;
    }

    [HttpPost(Name = "CriarProduto")]
    public Task<IActionResult> Criar(string nome, decimal taxaJurosAnual, int prazoMaximoMeses)
    {
        var sucesso = _emprestimoService.CriarProduto(nome, taxaJurosAnual, prazoMaximoMeses);
        if (!sucesso)
        {
            return Task.FromResult<IActionResult>(BadRequest("Não foi possível criar o produto."));
        }
        return Task.FromResult<IActionResult>(Ok());
    }

    [HttpGet(Name = "ObterProduto")]
    public Task<IActionResult> Obter(int id)
    {
        var produto = _emprestimoService.ObterProduto(id);
        if (produto == null)
        {
            return Task.FromResult<IActionResult>(NotFound("Produto nao encontrado."));
        }
        return Task.FromResult<IActionResult>(Ok(produto));
    }

    [HttpDelete(Name = "DeletarProduto")]
    public Task<IActionResult> Deletar(int id)
    {
        var sucesso = _emprestimoService.DeletarProduto(id);
        if (!sucesso)
        {
            return Task.FromResult<IActionResult>(NotFound());
        }
        return Task.FromResult<IActionResult>(Ok());
    }

    [HttpPatch(Name = "AlterarProduto")]
    public Task<IActionResult> Alterar(int id, string? nome, decimal? taxaJurosAnual, int? prazoMaximoMeses)
    {
        var sucesso = _emprestimoService.AlterarProduto(id, nome, taxaJurosAnual, prazoMaximoMeses);
        if (!sucesso)
        {
            return Task.FromResult<IActionResult>(NotFound());
        }
        return Task.FromResult<IActionResult>(Ok());
    }

    
}



