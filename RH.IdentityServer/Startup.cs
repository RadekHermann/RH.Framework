
using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using RH.IdentityServer.Inftrastructure;
using RH.IdentityServer.Inftrastructure.Data.Identity;

namespace RH.IdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("RHIdentity");
            string migrationAssembly = "RH.IdentityServer.Inftrastructure";

            services.AddControllers();

            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(connectionString, options => options.MigrationsAssembly(migrationAssembly));
            });

            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

            var identityBuilder = services.AddIdentityServer(options =>
            {
                options.UserInteraction.LoginUrl = "/account/login";
                options.UserInteraction.LogoutUrl = "/account/logout";
            })
                .AddOperationalStore<AppPersistedGrantDbContext>(options =>
                {
                    options.ConfigureDbContext = conf => conf.UseSqlServer(connectionString, options => options.MigrationsAssembly(migrationAssembly));

                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                })
                .AddConfigurationStore<AppConfigurationDbContext>(options =>
                {
                    options.ConfigureDbContext = conf => conf.UseSqlServer(connectionString, options => options.MigrationsAssembly(migrationAssembly));
                })
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<AppUser>();

            if (Environment.IsDevelopment())
            {
                identityBuilder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("Need to confgure key");
            }

            services.AddSpaStaticFiles(options => options.RootPath = "ClientApp/dist");
            services.AddResponseCompression();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseResponseCompression();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4400");
                }
            });
        }
    }
}
