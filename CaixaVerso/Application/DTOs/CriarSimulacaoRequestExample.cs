using Swashbuckle.AspNetCore.Filters;
using CaixaVerso.Application.DTOs;

namespace CaixaVerso.Documentation
{
    public class CriarSimulacaoRequestExample : IExamplesProvider<CriarSimulacaoRequest>
    {
        public CriarSimulacaoRequest GetExamples()
        {
            return new CriarSimulacaoRequest
            {
                ClienteId = 1,
                Valor = 5000.00m,
                PrazoMeses = 24,
                TipoProduto = "CDB_POS" // Exemplo de produto da Caixa
            };
        }
    }
}