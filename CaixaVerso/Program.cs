using Microsoft.EntityFrameworkCore;
using CaixaVerso.Infrastructure.Data;
using CaixaVerso.Infrastructure.Repositories;
using CaixaVerso.Domain.Interfaces;
using CaixaVerso.Domain.Services;
using CaixaVerso.Application.UseCases;
using System.Reflection;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Configurar Banco de Dados ---
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsEnvironment("Testing"))
    {
        options.UseInMemoryDatabase("TestDb");
    }
    else
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
});

// --- 2. Registrar Dependências ---
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ISimulacaoRepository, SimulacaoRepository>();
builder.Services.AddScoped<ICalculadoraInvestimento, CalculadoraInvestimento>();
builder.Services.AddScoped<CriarSimulacaoUseCase>();
builder.Services.AddScoped<ListarSimulacoesUseCase>();
builder.Services.AddScoped<ObterSimulacaoPorIdUseCase>();

builder.Services.AddControllers();

// --- 3. Swagger Config ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // ✅ O segredo: 'new()' injeta o tipo correto sem você escrever 
    // "Microsoft.OpenApi.Models", evitando o erro CS0234
    options.SwaggerDoc("v1", new() 
    { 
        Title = "CaixaVerso API", 
        Version = "v1",
        Description = "API de Simulação de Investimentos"
    });

    options.ExampleFilters();

    // Comentários XML
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        options.IncludeXmlComments(xmlPath);
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

var app = builder.Build();

// --- 4. Pipeline de Requisição (Middleware) ---

// ✅ O Swagger DEVE vir antes de quase tudo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // ✅ Tente deixar o padrão primeiro
    app.UseSwaggerUI(c =>
    {
        // ✅ Teste este caminho relativo, que é o mais compatível:
        c.SwaggerEndpoint("swagger/v1/swagger.json", "CaixaVerso API v1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }