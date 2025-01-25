using mail_api.Data;
using mail_api.Service;
using mail_api.Domain.@interface;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient();




builder.Services.AddScoped<ICepService, CepService>();
builder.Services.AddScoped<ICepRepository, CepRepository>();


builder.Services.AddControllers();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();  
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();

