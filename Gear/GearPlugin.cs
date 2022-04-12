using System;
using System.Linq;
using System.Threading.Tasks;
using BrackeysBot.API.Plugins;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
    private IHost _host;

    /// <inheritdoc />
    protected override Task OnLoad()
    {
        _host = CreateHostbuilder().Build();
        return Task.WhenAll(_host.StartAsync(), base.OnLoad());
    }

    /// <inheritdoc />
    protected override Task OnUnload()
    {
        return Task.WhenAll(_host.StopAsync(), base.OnUnload());
    }

    public override void Dispose()
    {
        _host.Dispose();
        base.Dispose();
    }

    static IHostBuilder CreateHostbuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureWebHost(host =>
                host.UseKestrel()
                    .UseStartup<Startup>());
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        // Startup.ConfigureServices(services);
        base.ConfigureServices(services);
    }
}