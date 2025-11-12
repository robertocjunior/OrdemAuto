using Bussiness.Services;
using Domain.Interfaces;
using Infra.Contexts;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ==================================================================
// 1. CONFIGURAÇÃO CORS MAIS ESPECÍFICA
// ==================================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins(
                    "http://localhost:3000",
                    "http://localhost:80", 
                    "http://localhost"
                )
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
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
// 2. APLICAR MIGRAÇÕES
// ==================================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AnalyzerDbContext>();
        context.Database.Migrate();
        Console.WriteLine("Migrações do banco de dados aplicadas com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao aplicar migrações: {ex.Message}");
    }
}

// ==================================================================
// 3. CONFIGURAÇÃO DO PIPELINE - CORS PRIMEIRO!
// ==================================================================

// CORS DEVE SER O PRIMEIRO MIDDLEWARE
app.UseCors("AllowFrontend");

// Swagger
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // Mantenha comentado por enquanto
app.UseAuthorization();
app.MapControllers();

app.Run();