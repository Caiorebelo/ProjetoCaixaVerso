using Xunit;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using CaixaVerso.Application.DTOs;

public class SimulacoesEndpointTests 
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public SimulacoesEndpointTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task POST_Simulacoes_Deve_Retornar_200()
    {
        var request = new CriarSimulacaoRequest
        {
            ClienteId = 1,
            Valor = 1000,
            PrazoMeses = 12,
            TipoProduto = "CDB"
        };

        var response = await _client.PostAsJsonAsync("/api/simulacoes", request);

        response.EnsureSuccessStatusCode();

        // ✅ Agora usamos CriarSimulacaoResponse
        var result = await response.Content
                                   .ReadFromJsonAsync<CriarSimulacaoResponse>();

        Assert.NotNull(result);
        Assert.True(result!.ResultadoSimulacao.ValorFinal > 0);
        Assert.Equal(12, result.ResultadoSimulacao.PrazoMeses);
    }

    [Fact]
    public async Task GET_Simulacoes_Por_Id_Deve_Retornar_200()
    {
        // Arrange - cria a simulação primeiro
        var request = new CriarSimulacaoRequest
        {
            ClienteId = 1,
            Valor = 1000,
            PrazoMeses = 12,
            TipoProduto = "CDB"
        };

        var postResponse = await _client
            .PostAsJsonAsync("/api/simulacoes", request);

        postResponse.EnsureSuccessStatusCode();

        var resultado = await postResponse.Content
            .ReadFromJsonAsync<CriarSimulacaoResponse>();

        Assert.NotNull(resultado);
        Assert.True(resultado!.ResultadoSimulacao.ValorFinal > 0);

        // Act - busca pelo Id
        var getResponse = await _client
            .GetAsync($"/api/simulacoes/{resultado.ProdutoValidado.Id}");

        getResponse.EnsureSuccessStatusCode();

        // ✅ GET retorna SimulacaoResponse
        var simulacao = await getResponse.Content
            .ReadFromJsonAsync<SimulacaoResponse>();

        Assert.NotNull(simulacao);
        Assert.Equal(resultado.ResultadoSimulacao.ValorFinal, simulacao!.ValorFinal);
        Assert.Equal(resultado.ResultadoSimulacao.PrazoMeses, simulacao.PrazoMeses);
    }
}
