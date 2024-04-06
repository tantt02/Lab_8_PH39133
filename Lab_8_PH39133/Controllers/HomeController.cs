using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab_8_PH39133.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
        {
            HttpContext.Session.SetString("name", "Phan Viet The");
            HttpContext.Session.SetString("email", "thepv@uit.edu.vn.com"); 
            return View();
		}
		public IActionResult About()
		{
			ViewBag.Name = HttpContext.Session.GetString("name");
			ViewBag.Email = HttpContext.Session.GetString("email");
			ViewData["Message"] = "Your about page, please refesh page after one minute";
			ViewData["Title"] = "Demo session login";
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
