using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using ProjetoMei.Back.Data;
using ProjetoMei.Back.Filters;
using ProjetoMei.Back.Repository;
using ProjetoMei.Back.Services;
using ProjetoMei.Front.Services;

// A pasta de arquivos estáticos (css, js, fonts, images) fica dentro do Front.
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    WebRootPath = "Front/wwwroot"
});

// ---- MVC: controllers da API (Back) e das páginas (Front) ----
// AutoValidateAntiforgeryToken: todo POST dos formulários exige o token CSRF.
// (O UsuarioMeiController da API tem [IgnoreAntiforgeryToken].)
builder.Services.AddControllersWithViews(opcoes =>
    opcoes.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

// As views Razor ficam em Front/Views (e não em Views/ na raiz).
builder.Services.Configure<RazorViewEngineOptions>(opcoes =>
{
    opcoes.ViewLocationFormats.Clear();
    opcoes.ViewLocationFormats.Add("/Front/Views/{1}/{0}.cshtml");
    opcoes.ViewLocationFormats.Add("/Front/Views/Shared/{0}.cshtml");
});

// Mantém acentos e emojis como estão no HTML (sem virar &#xE7; etc.).
builder.Services.AddWebEncoders(opcoes =>
    opcoes.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All));

// ---- Swagger: documenta só a API (não as páginas) ----
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opcoes =>
    opcoes.DocInclusionPredicate((_, api) =>
        api.ActionDescriptor is ControllerActionDescriptor d
        && d.ControllerTypeInfo.IsDefined(typeof(ApiControllerAttribute), true)));

// ---- Back: injeção de dependência e banco ----
// Scoped: uma instância por requisição, o ciclo de vida correto para quem usa o DbContext.
builder.Services.AddScoped<IUsuarioMeiService, UsuarioMeiService>();
builder.Services.AddScoped<IUsuarioMeiRepository, UsuarioMeiRepository>();
builder.Services.AddScoped<IAdministradorRepository, AdministradorRepository>();

// Banco MySQL (string de conexão em ConnectionStrings:MySql, no appsettings.json).
string conexaoMySql = builder.Configuration.GetConnectionString("MySql")
    ?? throw new InvalidOperationException("Falta ConnectionStrings:MySql no appsettings.json.");
builder.Services.AddDbContext<MeiDbContext>(opcoes =>
    opcoes.UseMySql(conexaoMySql, new MySqlServerVersion(new Version(8, 0, 36))));

// ---- Front: sessão (guarda o usuário logado) ----
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opcoes =>
{
    opcoes.Cookie.HttpOnly = true;
    opcoes.Cookie.IsEssential = true;
    opcoes.IdleTimeout = TimeSpan.FromHours(2);
});

// ---- Front: cliente da API (a API roda neste mesmo projeto) ----
// A barra no fim da URL é importante: os caminhos ("vitrine", "login", "3") são relativos a ela.
string urlApi = builder.Configuration["Api:BaseUrl"] ?? "http://localhost:5037/api/UsuarioMei/";
builder.Services.AddHttpClient<ApiClient>(cliente =>
{
    cliente.BaseAddress = new Uri(urlApi.EndsWith('/') ? urlApi : urlApi + "/");
    cliente.Timeout = TimeSpan.FromSeconds(10);

    // Chave que libera os endpoints internos da API (admin, listagem, edição, exclusão).
    cliente.DefaultRequestHeaders.Add(ChaveApiAttribute.Cabecalho, builder.Configuration["Api:Chave"] ?? string.Empty);
});
builder.Services.AddScoped<VitrineService>();

var app = builder.Build();

// ---- Banco: cria banco/tabelas se não existirem e o primeiro administrador ----
using (IServiceScope escopo = app.Services.CreateScope())
{
    try
    {
        BancoInicializador.Inicializar(escopo.ServiceProvider, app.Configuration);
    }
    catch (Exception ex)
    {
        app.Logger.LogCritical(ex,
            "Não foi possível preparar o banco MySQL. Confira se o MySQL está rodando e se " +
            "ConnectionStrings:MySql (servidor, usuário e senha) está correta no appsettings.json.");
        throw;
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();       // /swagger
    app.UseSwaggerUI();
}

app.UseStaticFiles();       // Front/wwwroot
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.MapControllers();

app.Run();