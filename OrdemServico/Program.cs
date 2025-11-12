using Bussiness.Services;
using Domain.Interfaces;
using Infra.Contexts;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ==================================================================
// 1. CONFIGURAÇÃO CORS MELHORADA
// ==================================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()  // Permite qualquer origem (em desenvolvimento)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IOrdemServicoService, OrdemServicoService>();
builder.Services.AddScoped<IOrdemServicoRepository, OrdemServicoRepository>();
builder.Services.AddScoped<ICadastroService, CadastroService>();
builder.Services.AddScoped<ICadastroRepository, CadastroRepository>();
builder.Services.AddScoped<IParceiroService, ParceiroService>();
builder.Services.AddScoped<IParceiroRepository, ParceiroRepository>();

// Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AnalyzerDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// ==================================================================
// 2. CONFIGURAÇÃO DO PIPELINE
// ==================================================================

// CORS DEVE VIR ANTES DE OUTROS MIDDLEWARES
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();