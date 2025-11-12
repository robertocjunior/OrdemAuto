var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// app.UseHttpsRedirection(); // Mantenha comentado
app.UseStaticFiles(); 
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// --- GARANTA QUE ESTA LINHA ESTÁ AQUI ---
app.MapFallbackToFile("index.html");
// --- FIM DA ADIÇÃO ---

app.Run();