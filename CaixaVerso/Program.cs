using Microsoft.EntityFrameworkCore;
using CaixaVerso.Infrastructure.Data;
using CaixaVerso.Infrastructure.Repositories;
using CaixaVerso.Domain.Interfaces;
using CaixaVerso.Domain.Services;
using CaixaVerso.Application.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Configurar banco de dados (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar Repositórios (Infraestrutura)
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ISimulacaoRepository, SimulacaoRepository>();

// Registrar Serviço de Cálculo (Domain)
builder.Services.AddScoped<ICalculadoraInvestimento, CalculadoraInvestimento>();

// Registrar UseCase (Application)
builder.Services.AddScoped<CriarSimulacaoUseCase>();
builder.Services.AddScoped<ListarSimulacoesUseCase>();

// Adicionar Controllers
builder.Services.AddControllers();

// Swagger (para testar API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do ambiente
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar HTTPS
app.UseHttpsRedirection();

// Mapear Controllers
app.MapControllers();

// Rodar aplicação
app.Run();