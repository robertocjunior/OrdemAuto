using Bussiness.Services;
using Domain.Interfaces;
using Infra.Contexts;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using OrdemServico.Utils;

var builder = WebApplication.CreateBuilder(args);

// ==================================================================
// 1. DEFINIR A POLÍTICA DE CORS
// ==================================================================
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost", // A porta 80 (do docker-compose web)
                                             "http://localhost:80", 
                                             "http://localhost:3000") // Porta padrão do React (para dev local)
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});
// ==================================================================


// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ResponseUtility));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// INJEO DE DEPENDENCIA
builder.Services.AddScoped<IOrdemServicoService, OrdemServicoService>();
builder.Services.AddScoped<IOrdemServicoRepository, OrdemServicoRepository>();
builder.Services.AddScoped<ICadastroService, CadastroService>();
builder.Services.AddScoped<ICadastroRepository, CadastroRepository>();
builder.Services.AddScoped<IParceiroService, ParceiroService>();
builder.Services.AddScoped<IParceiroRepository, ParceiroRepository>();

// BANCO DE DADOS
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AnalyzerDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction()) // Permitir swagger em prod
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ==================================================================
// 2. USAR A POLÍTICA DE CORS
// ==================================================================
app.UseCors(MyAllowSpecificOrigins);
// ==================================================================


app.UseAuthorization();

app.MapControllers();

app.Run();