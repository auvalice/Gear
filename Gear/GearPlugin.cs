using System;
using System.ComponentModel.Design;
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
    public static string ConfigSchema = @"
{
  ""$schema"": ""https://json-schema.org/draft/2020-12/schema"",
    ""$id"": ""https://example.com/product.schema.json"",
    ""title"": ""Gear-Configuration"",
    ""description"": ""JSON Schema for the Gear BrackeysBot plugin"",
    ""type"": ""object"",
    ""properties"": {
        ""runOnLoad"": {
            ""type"": ""boolean"",
            ""description"": ""Whether the plugin should be enabled on bot startup""
        }
    },
""required"": [
""runOnLoad""
    ]
}
";
    
    private WebApplication? _host;

    /// <inheritdoc />
    protected override Task OnLoad()
    {
        var builder = WebApplication.CreateBuilder();
        BindServices(builder);
        _host = builder.Build();
        _host.MapControllers();
        return base.OnLoad();
    }

    /// <inheritdoc />
    protected override Task OnEnable()
    {
        return Task.WhenAll(_host!.StartAsync(), base.OnLoad());
    }

    /// <inheritdoc />
    protected override Task OnDisable()
    {
        return Task.WhenAll(_host!.StopAsync(), base.OnUnload());
    }

    private void BindServices(WebApplicationBuilder builder)
    {
        builder.Services.AddRouting();
        builder.Services.AddMvc().AddApplicationPart(typeof(GearPlugin).Assembly).AddControllersAsServices();
        builder.Services.AddSingleton<IPluginService, PluginService>();
    }
}