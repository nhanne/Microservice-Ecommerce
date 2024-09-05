using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.API.Controllers;

//[ApiController]
//[ApiVersion("1.0")]
//[Route("api/{api_version:apiVersion}/[controller]")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}