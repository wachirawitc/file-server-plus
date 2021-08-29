using FileServerPlus.Mvc.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace DemoMulti
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            var rootDirectory1 = new DirectoryInfo("..\\storages\\storage1\\");
            app.UseFileServerPlus("Server1", rootDirectory1, "/storage1", enableDirectoryBrowsing: true);

            var rootDirectory2 = new DirectoryInfo("..\\storages\\storage2\\");
            app.UseFileServerPlus("Server2", rootDirectory2, "/storage2", enableDirectoryBrowsing: true);

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