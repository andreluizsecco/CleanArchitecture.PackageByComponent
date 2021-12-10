using CleanArchitecture.PackageByComponent.Core.Data.Repositories;
using CleanArchitecture.PackageByComponent.Core.Domain.Interfaces.Repositories;
using CleanArchitecture.PackageByComponent.Core.Domain.Interfaces.Services;
using CleanArchitecture.PackageByComponent.Core.Domain.Services;
using DotNet.DynamicInjector;
using DotNet.DynamicInjector.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;

namespace CleanArchitecture.PackageByComponent.Web
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
            services.AddControllersWithViews();

            services.AddScoped<IHomePageRepository, HomePageRepository>();
            services.AddScoped<IHomePageService, HomePageService>();

            var role = new IoCRole
            {
                Active = true,
                Dll = "CleanArchitecture.PackageByComponent.Customer.Data.dll", //DLL name
                Implementation = "CleanArchitecture.PackageByComponent.Customer.Data", // Implementation name, can be used for a control if you use several projects and wanted to separate them
                Priority = 1, // Priority that the dll should be loaded
                LifeTime = LifeTime.SCOPED, // Lifetime of your addiction injection
                Name = "My custom implementation for my customer" //Dependency name. It is used only for identification,
            };
            var ioCConfiguration = new IoCConfiguration();
            ioCConfiguration.AddAllowedInterfaceNamespace("CleanArchitecture.PackageByComponent");
            ioCConfiguration.AddRole(role);
            services.RegisterDynamicDependencies(ioCConfiguration);

            services.AddFeatureManagement();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
