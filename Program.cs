using mail_api.Data;
using mail_api.Interfaces;
using mail_api.InternalInterface;
using mail_api.Service;

var builder = WebApplication.CreateBuilder(args);

// Registra IHttpClientFactory
builder.Services.AddHttpClient();

// Registrar ICepService com CepService
builder.Services.AddScoped<ICepService, CepService>();

// Registrar ICepRepository com CepRepository
builder.Services.AddScoped<ICepRepository, CepRepository>();

// Adicionando suporte para controllers (API)
builder.Services.AddControllers();

var app = builder.Build();

// Configuração do middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();  // Habilita HTTP Strict Transport Security em produção
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();

