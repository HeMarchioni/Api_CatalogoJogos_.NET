using ApiCatalogoJogos.Repositories;
using ApiCatalogoJogos.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            services.AddScoped<IJogoService, JogoService>();   // -> injeção de dependecia do construtor para poder intanciar e trabalhar com a interface (que vai representar a classe definida)
            services.AddScoped<IJogoRepository, JogoSqlServerRepository>();



            // CicloDeVida  (Tipos de injeção de Dependencia)

            //services.AddSingleton<IExemploSingleton, ExemploCicloDeVida>();    --> É criada uma unica instancia ela se mantem assim que a aplicação é iniciada
            //services.AddScoped<IExemploScoped, ExemploCicloDeVida>();          --> É criada uma instancia unica toda vez q a rota do controlador é chamada depois é fechada
            //services.AddTransient<IExemploTransient, ExemploCicloDeVida>();    --> É criada uma intancia para cada chamada individualmete dentro do controlador depois fechada



            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiCatalogoJogos", Version = "v1" });
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
