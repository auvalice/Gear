using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gear;

public class Startup
{
    public static void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute("/api", "/api/{controller}/{action}");
        });
    }
    
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddRouting();
        services.AddControllers();
        
        services.AddSingleton<IPluginService, PluginService>();
    }
}