using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EFCore2019.Api.AutofactModules;
using EFCore2019.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace EFCore2019
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();

            Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(Configuration)
                        //.WriteTo.Sentry("https://f65f908fde9940f0b2b16c2045643ea9@sentry.io/1260343")
                        .Enrich.FromLogContext()
                        .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info { Title = "EFCore 2019 v1", Version = "V1" });
                config.IncludeXmlComments(string.Format("{0}\\EFCore2019.Api.xml", AppDomain.CurrentDomain.BaseDirectory));
                config.IncludeXmlComments(string.Format("{0}\\EFCore2019.Domain.xml", AppDomain.CurrentDomain.BaseDirectory));
            });

            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            var builder = new ContainerBuilder();
            services.AddCors();
            builder.Populate(services);
            builder.RegisterModule(new DomainModule());
            var container = builder.Build();

            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors(builder => builder
                   .AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(config => { config.SwaggerEndpoint("v1/swagger.json", "PCC1 PMS Api v1"); config.DocExpansion(DocExpansion.None); config.ShowExtensions(); });
        }
    }
}
