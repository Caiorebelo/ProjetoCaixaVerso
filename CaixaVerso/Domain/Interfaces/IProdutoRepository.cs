using CaixaVerso.Domain.Entities;

namespace CaixaVerso.Domain.Interfaces;

public interface IProdutoRepository
{
    Task<Produto?> ObterPorId(int id);
    Task<Produto?> ObterPorTipo(string tipo);
}