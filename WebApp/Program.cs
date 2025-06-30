using WebApp.Mappings;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC + API Controllers
builder.Services.AddControllersWithViews();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Serviços (arquivos .txt simulando BD)
builder.Services.AddSingleton<PessoaService>();
builder.Services.AddSingleton<CandidaturaService>();
builder.Services.AddSingleton<VagaService>();

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5018/");
});


var app = builder.Build();

// Pipeline HTTP padrão
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// HTTPS e arquivos estáticos
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Rota padrão MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Rota da API REST (para segurança, pode ser opcional)
app.MapControllers(); // 👈 NECESSÁRIO para funcionar a [ApiController]

app.Run();
