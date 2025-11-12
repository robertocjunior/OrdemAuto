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
// (NOVA ADIÇÃO) APLICAR MIGRATIONS AUTOMATICAMENTE
// ==================================================================
// Este bloco garante que o banco de dados seja criado e atualizado
// com as migrations antes da API começar a rodar.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    DRAFTING
    {
        var context = services.GetRequiredService<AnalyzerDbContext>();
        
        // Espera um pouco para garantir que o container do DB esteja 100% pronto
        // (O 'depends_on' no docker-compose só garante que o container iniciou,
        // não que o Postgres está pronto para aceitar conexões)
        System.Threading.Thread.Sleep(5000); // 5 segundos

        // Aplica migrations pendentes e cria o banco se não existir
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        // Em um app real, use um logger (ILogger).
        Console.WriteLine("========================================");
        Console.WriteLine("ERRO AO APLICAR MIGRATIONS:");
        Console.WriteLine(ex.Message);
        Console.WriteLine("========================================");
        // Você pode querer parar a aplicação se o DB for essencial
        // throw;
    }
}


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