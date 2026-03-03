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

        var result = await response.Content
                                   .ReadFromJsonAsync<SimulacaoResultadoResponse>();

        Assert.NotNull(result);
        Assert.True(result!.ValorFinal > 0);
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
        .ReadFromJsonAsync<SimulacaoResultadoResponse>();

    Assert.NotNull(resultado);
    Assert.True(resultado!.Id > 0); // 🔎 valida se Id foi gerado

    // Act - busca pelo Id
    var getResponse = await _client
        .GetAsync($"/api/simulacoes/{resultado.Id}");

    // Assert
    getResponse.EnsureSuccessStatusCode();

    var simulacao = await getResponse.Content
        .ReadFromJsonAsync<SimulacaoResultadoResponse>();

    Assert.NotNull(simulacao);
    Assert.Equal(resultado.Id, simulacao!.Id);
}
}