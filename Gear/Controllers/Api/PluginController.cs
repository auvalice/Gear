using System.Linq;
using System.Text.Json;
using Gear.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Gear.Controllers.Api;

[ApiController]
public class HomeController : Controller
{
    private PluginService _pluginService = new();

    [HttpGet]
    [Route("/api/plugins")]
    public IActionResult LoadedPlugins()
    {
        var loadedPlugins = _pluginService.GetLoadedPlugins().Distinct();
        return Json(new {plugins = loadedPlugins.ToArray()});
    }
}