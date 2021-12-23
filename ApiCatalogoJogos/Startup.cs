using ApiCatalogoJogos.Middleware;
using ApiCatalogoJogos.Repositories;
using ApiCatalogoJogos.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace ApiCatalogoJogos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IJogoService, JogoService>();   // -> inje��o de dependecia do construtor para poder intanciar e trabalhar com a interface (que vai representar a classe definida)
            services.AddScoped<IJogoRepository, JogoSqlServerRepository>();



            // CicloDeVida  (Tipos de inje��o de Dependencia)

            //services.AddSingleton<IExemploSingleton, ExemploCicloDeVida>();    --> � criada uma unica instancia ela se mantem assim que a aplica��o � iniciada
            //services.AddScoped<IExemploScoped, ExemploCicloDeVida>();          --> � criada uma instancia unica toda vez q a rota do controlador � chamada depois � fechada
            //services.AddTransient<IExemploTransient, ExemploCicloDeVida>();    --> � criada uma intancia para cada chamada individualmete dentro do controlador depois fechada



            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiCatalogoJogos", Version = "v1" });

                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                c.IncludeXmlComments(Path.Combine(basePath, fileName));

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiCatalogoJogos v1"));
            }

            app.UseMiddleware<ExceptionMiddleware>();  // -> Para Utilizar o Middleware que eu criei.

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
