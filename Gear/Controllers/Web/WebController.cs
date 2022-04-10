using Microsoft.AspNetCore.Mvc;

namespace Gear.Controllers.Web;

public class HomeController : Controller
{
    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {
        return new RedirectResult("~/index.html");
    }
}