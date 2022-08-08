using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosAPI.Models;
using UsuariosAPI.Services;

namespace UsuariosAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string _policyName = "CorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy(name: _policyName, builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddDbContext<UserDbContext>(opt => opt.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("UsuarioConexao"))); //configurar o sql  server

            //configurar o identity
            services.AddIdentity<CustomIdentityUser, IdentityRole<int>>(
                opt => opt.SignIn.RequireConfirmedEmail = true //requerendo confirmação de e-mail - o usuario nao pode fazer login sem confirmar o e-mail
                ).AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders(); //autorizando token de confirmação de e-mail

            //isso desabilita os requisitos de ter uma letra maiúscula e um caracter especial na senha do cadastro do usuario
            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequiredLength = 8;
            //});

            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //utilizar o auto mapper
            services.AddTransient<UsuarioService>(); //ativa a classe como service
            services.AddTransient<TokenService>(); //ativa a classe como service
            services.AddTransient<EmailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_policyName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
