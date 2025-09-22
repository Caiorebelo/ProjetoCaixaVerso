using ProjetoCaixaVerso.Services;
using Microsoft.EntityFrameworkCore;
using ProjetoCaixaVerso.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlite("Data Source=dbcaixaverso.db"));
builder.Services.AddScoped<IProdutoService,ProdutoService>();
builder.Services.AddScoped<IEmprestimoService,EmprestimoService>();
builder.Services.AddScoped<IProdutoRepository,ProdutoRepository>();
builder.Services.AddControllers();
builder.Services.AddScoped<Db>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddOpenApiDocument();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Db>();
    await db.InitializeAsync();
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
