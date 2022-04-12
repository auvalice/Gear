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
    public JsonResult Index()
    {
        return new JsonResult("{data: [\"OK!\"]");
    }
}