using Bussiness.Services;
using Domain.Interfaces;
using Infra.Contexts;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

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

// Database com retry policy
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AnalyzerDbContext>(options =>
    options.UseNpgsql(connectionString, 
        npgsqlOptions => npgsqlOptions.EnableRetryOnFailure()));

var app = builder.Build();

// ==================================================================
// MIGRAÇÕES COM TENTATIVAS MÚLTIPLAS
// ==================================================================
await ApplyMigrationsWithRetry(app);

app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();

app.Run();

async Task ApplyMigrationsWithRetry(WebApplication app)
{
    const int maxRetries = 5;
    const int delayMs = 5000;
    
    for (int i = 0; i < maxRetries; i++)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        
        try
        {
            var context = services.GetRequiredService<AnalyzerDbContext>();
            Console.WriteLine($"Tentativa {i + 1} de {maxRetries}: Aplicando migrações...");
            
            await context.Database.MigrateAsync();
            Console.WriteLine("✅ Migrações aplicadas com sucesso!");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Tentativa {i + 1} falhou: {ex.Message}");
            
            if (i < maxRetries - 1)
            {
                Console.WriteLine($"Aguardando {delayMs/1000} segundos antes da próxima tentativa...");
                await Task.Delay(delayMs);
            }
            else
            {
                Console.WriteLine("⚠️  Não foi possível aplicar migrações. A API iniciará sem banco.");
            }
        }
    }
}