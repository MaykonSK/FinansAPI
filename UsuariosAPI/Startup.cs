using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using UsuariosAPI.Models;
using UsuariosAPI.Services;
using UsuariosAPI.Shared;

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
            services.AddDbContext<FinansDBContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("FinansConexao"))); //outro banco

            //configurar o identity
            services.AddIdentity<CustomIdentityUser, IdentityRole<int>>(
                opt => opt.SignIn.RequireConfirmedEmail = true //requerendo confirma??o de e-mail - o usuario nao pode fazer login sem confirmar o e-mail
                ).AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders(); //autorizando token de confirma??o de e-mail

            //isso desabilita os requisitos de ter uma letra mai?scula e um caracter especial na senha do cadastro do usuario
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
            services.AddTransient<EmailService>(); //ativa a classe como service
            services.AddTransient<RecaptchaValidate>(); //ativa a classe como service
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
