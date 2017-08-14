using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Peliculas.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Peliculas
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("MySql");
            // Add framework services.
            services.AddDbContext<PeliculasContext>(options =>
                options.UseMySql(connectionString)
            );
            services.Configure<AuthOptions>(Configuration.GetSection("AuthenticationSettings"));
            //Se deben especificar los servicios inyectables
            //Dependiendo del caso se puede utilizar AddSingleton o AddTransient
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPeliculasService, PeliculasService>();
            services.AddScoped<IPersonasService, PersonasService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                Audience = "Public",
                AutomaticChallenge = true,
                AutomaticAuthenticate = true,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = "PeliculasAPI",
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AuthenticationSettings:SigningKey").Value))
                }
            });

            app.UseMvc();
        }
    }
}
