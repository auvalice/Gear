using System.Collections.Immutable;
using System.IO;
using System.Threading.Tasks;
using BrackeysBot.API.Plugins;
using Gear.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Gear;

/// <summary>
/// <para>
///     Entry point for the Gear BrackeysBot plugin.
/// </para>
/// <para>
///     This plugin enables users to configure other BrackeysBot loaded plugins via a web interface. The web
///     interface is written in a separate repository and sends calls to the API exposed by this plugin.
///     The API is generated and served at runtime with Kestrel.
/// </para>
/// </summary>
[Plugin("Gear")]
[PluginDescription("Configure other plugins via a web interface")]
public sealed class GearPlugin : MonoPlugin
{
    private WebApplication? _host;

    /// <inheritdoc />
    protected override Task OnLoad()
    {
        _host = WebApplication.CreateBuilder()
            .ConfigureServices(ServiceConfiguration)
            .Build();
        
        ConfigureHost(_host);

        return base.OnLoad();
    }

    /// <inheritdoc />
    protected override Task OnEnable()
    {
        return _host!.StartAsync();
    }

    /// <inheritdoc />
    protected override Task OnDisable()
    {
        return _host!.StopAsync();
    }

    private void ServiceConfiguration(IServiceCollection services)
    {
        services.AddRouting();
        services.AddControllers().AddApplicationPart(typeof(GearPlugin).Assembly);
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });
        services.AddSingleton<IPluginService, PluginService>();
    }

    private void ConfigureHost(WebApplication host)
    {
        host.MapControllers();
        host.UseDefaultFiles();
        host.UseStaticFiles();
        host.UseApiVersioning();
        host.UseRouting();
        host.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute("api/v1", "api/v1/{controller}/{action}");

        });
    }
}