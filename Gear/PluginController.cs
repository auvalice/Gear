using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Gear;

[ApiController]
public class PluginController : ControllerBase
{
    private IPluginService _pluginService;
    
    public PluginController(IPluginService pluginService)
    {
        _pluginService = pluginService;
    }

    [HttpGet]
    [Route("/api")]
    public JsonResult Get()
    {
        return new JsonResult(new {data = "OK"});
    }

    [HttpGet]
    [Route("/api/plugins")]
    public JsonResult Plugins()
    {
        var loadedPlugins = _pluginService.GetLoadedPlugins();
        var pluginNames = loadedPlugins.Select(plugin => plugin.Name).ToArray();
        return new JsonResult(new {plugins = pluginNames});
    }

    [HttpGet]
    [Route("/api/configs")]
    public JsonResult Configs()
    {
        var pluginConfigs = _pluginService.GetConfigs();
        return new JsonResult(new {configs = pluginConfigs});
    }
}