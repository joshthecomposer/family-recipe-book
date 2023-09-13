using Microsoft.AspNetCore.Mvc;

namespace MyApp.Controllers;

public class PublicController : Controller
{
    [HttpGet]
    public ViewResult Index()
	{
		return View();
	}
}
