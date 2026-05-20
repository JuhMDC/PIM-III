// DOC: Configuração inicial do backend e serviços do ASP.NET Core
using Microsoft.EntityFrameworkCore;
using LuxxMotorsAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Configura a conexão com o banco de dados SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Adiciona suporte para Controllers (que criaremos no próximo passo)
builder.Services.AddControllers();

// 3. Permite que seu HTML acesse a API (CORS)
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Adicione isto:
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// E adicione isto logo após o 'var app = builder.Build();':
app.UseCors("PermitirTudo");


// 4. Configura o uso do CORS e dos Controllers
app.UseCors("AllowAll");
app.MapControllers();

app.Run();