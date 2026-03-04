using Microsoft.EntityFrameworkCore;
using CaixaVerso.Infrastructure.Data;
using CaixaVerso.Infrastructure.Repositories;
using CaixaVerso.Domain.Interfaces;
using CaixaVerso.Domain.Services;
using CaixaVerso.Application.UseCases;
using System.Reflection;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Configurar banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsEnvironment("Testing"))
    {
        options.UseInMemoryDatabase("TestDb");
    }
    else
    {
        options.UseSqlite(
            builder.Configuration.GetConnectionString("DefaultConnection"));
    }
});

// Registrar Repositórios
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ISimulacaoRepository, SimulacaoRepository>();

// Registrar Serviço de Cálculo
builder.Services.AddScoped<ICalculadoraInvestimento, CalculadoraInvestimento>();

// Registrar UseCases
builder.Services.AddScoped<CriarSimulacaoUseCase>();
builder.Services.AddScoped<ListarSimulacoesUseCase>();
builder.Services.AddScoped<ObterSimulacaoPorIdUseCase>();

builder.Services.AddControllers();

// ✅ Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Inclui comentários XML para enriquecer a documentação
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }

    // Configuração básica de título/versão
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "CaixaVerso API",
        Version = "v1",
        Description = "API para simulações de investimentos (CaixaVerso)."
    });
});

var app = builder.Build();

// ✅ Swagger UI habilitado em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CaixaVerso API v1");
        c.RoutePrefix = string.Empty; // abre direto em https://localhost:5001/
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }
