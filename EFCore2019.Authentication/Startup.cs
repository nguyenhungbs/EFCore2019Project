using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using EFCore2019.Authentication.Configuration;
using EFCore2019.Authentication.IdentityServer;
using EFCore2019.Domain.Entities;
using EFCore2019.Domain.Repositories;
using EFCore2019.Domain.Services.Roles;
using EFCore2019.Domain.Services.Roles.Implementation;
using EFCore2019.Domain.Services.Users;
using EFCore2019.Domain.Services.Users.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EFCore2019.Authentication
{
    public class Startup
    {
        public IConfiguration _configuration { get; private set; }

        public IHostingEnvironment _environment { get; private set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration;

            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();

            _configuration = builder.Build();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            var appSettingSection = _configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);
            var appSettings = new AppSettings();
            appSettingSection.Bind(appSettings);

            //CONFIGURE IDENTITY SERVER 4 WITH IN-MEMORY STORES, KEYS, CLIENTS AND SCOPES

            //1. Identity server 4 cert
            var cert = new X509Certificate2(Path.Combine(_environment.ContentRootPath, "IdentityServer", "idsrv3test.pfx"), "idsrv3test");

            //2. Inject storage user service
            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRolesService, RoleService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();

            services.AddIdentityServer()
                .AddSigningCredential(cert)
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients(appSettings))
                .AddProfileService<ProfileService>();

            //3. Inject the classes we just created





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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
