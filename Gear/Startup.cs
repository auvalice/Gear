using BrackeysBot.API.Extensions;
using BrackeysBot.API.Plugins;
using Gear.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Gear;

/// <summary>
///     Startup class for Kestrel self host. Mimicks Startup class in default ASP.NET Web applications.
///     Misses the final call to <c>Run()</c>, as this is handled in the main <see cref="GearPlugin"/> file.
/// </summary>
public class Startup
{
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        // app.UseEndpoints(endpoints =>
        // {
        //     endpoints.MapControllerRoute(
        //         name: "default",
        //         pattern: "{controller=Home}/{action=Index}/{id?}");
        // });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRouting();

        services.AddAuthentication();
        services.AddAuthorization();

        services.AddControllers();



    }
}