using ApiCatalogoJogos.Data;
using ApiCatalogoJogos.Middleware;
using ApiCatalogoJogos.Repositories;
using ApiCatalogoJogos.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExemploApiCatalogoJogos", Version = "v1" });

    var basePath = AppDomain.CurrentDomain.BaseDirectory;
    var fileName = typeof(Program).GetTypeInfo().Assembly.GetName().Name + ".xml";
    c.IncludeXmlComments(Path.Combine(basePath, fileName));
});


builder.Services.AddScoped<IJogoService, JogoService>();   // -> injeção de dependecia do construtor para poder intanciar e trabalhar com a interface (que vai representar a classe definida)
builder.Services.AddScoped<IJogoRepository, JogoSqlServerRepository>();




var configuration = builder.Configuration;  //-> Obter o IConfiguration para pegar a String de conexão


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("Default"), x => x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName).EnableRetryOnFailure());
});




// CicloDeVida  (Tipos de injeção de Dependencia)

//services.AddSingleton<IExemploSingleton, ExemploCicloDeVida>();    --> É criada uma unica instancia ela se mantem assim que a aplicação é iniciada
//services.AddScoped<IExemploScoped, ExemploCicloDeVida>();          --> É criada uma instancia unica toda vez q a rota do controlador é chamada depois é fechada
//services.AddTransient<IExemploTransient, ExemploCicloDeVida>();    --> É criada uma intancia para cada chamada individualmete dentro do controlador depois fechada


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiCatalogoJogos v1"));
}


app.UseMiddleware<ExceptionMiddleware>();  // -> Para Utilizar o Middleware que eu criei.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
