using System.IO;
using System.Text;
using System.Threading.Tasks;
using BrackeysBot.API.Plugins;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using NLog.Extensions.Logging;

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
    private IWebHost? _webHost;

    /// <inheritdoc />
    protected override Task OnLoad()
    {
        Logger.Info("Configuration file for web service not yet supported. Using default Kestrel settings...");

        _webHost = new WebHostBuilder()
            .UseUrls()
            .UseKestrel()
            .UseStartup<Startup>()
            .UseWebRoot($"{Directory.GetCurrentDirectory()}\\plugins\\Gear\\wwwroot")
            .UseContentRoot($"{Directory.GetCurrentDirectory()}\\plugins\\Gear\\wwwroot")
            .UseStaticWebAssets()
            .ConfigureLogging((_, logging) =>
            {
                logging.ClearProviders();
                logging.AddNLog();
            })
            .Build();

        Logger.Info("Web host will be online soon at http://localhost:5000");
        return Task.WhenAll(_webHost.StartAsync(), base.OnLoad());
    }

    /// <inheritdoc />
    protected override Task OnUnload()
    {
        return Task.WhenAll(_webHost!.StopAsync(), base.OnUnload());
    }

    public override void Dispose()
    {
        _webHost?.Dispose();
        base.Dispose();
    }
}