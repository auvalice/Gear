using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Gear.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/plugins")]
public class PluginController : ControllerBase
{
    private IPluginService _pluginService;
    
    public PluginController(IPluginService pluginService)
    {
        _pluginService = pluginService;
    }

    [MapToApiVersion("1.0")]
    [HttpGet]
    public JsonResult Plugins()
    {
        var loadedPlugins = _pluginService.GetLoadedPlugins();
        var pluginNames = loadedPlugins.Select(plugin => plugin.Name).ToArray();
        return new JsonResult(new {plugins = pluginNames});
    }
}