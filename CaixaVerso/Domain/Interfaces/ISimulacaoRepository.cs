using CaixaVerso.Domain.Entities;

namespace CaixaVerso.Domain.Interfaces;

public interface ISimulacaoRepository
{
    Task Adicionar(Simulacao simulacao);
    Task<List<Simulacao>> ListarPorCliente(int clienteId);
}