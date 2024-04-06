using Bai2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Bai2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Db_Lab8_B2 _context = new Db_Lab8_B2();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Login"] = HttpContext.Session.GetString("user");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string phoneNumber, string passWord)
        {
            var user = _context.Customers.FirstOrDefault(a => a.Password == passWord && a.PhoneNumber == phoneNumber);
            if (user != null)
            {
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
                ViewData["Login"] = true;
                return View("Information", user);
            }
            ViewData["wrong"] = "Nhập số điện thoại hoặc mật khẩu sai ";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [BindProperty]
        public Customers Customers { get; set; } = default!;
        [HttpPost]
        public IActionResult ProcessRegister()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (_context.Customers.Any(a => a.PhoneNumber == Customers.PhoneNumber))
            {
                ViewData["Valid"] = false;
                return View("Register");
            }
            var id = Guid.NewGuid();
            Customers.IdCustomers = id;

            _context.Customers.Add(Customers);

            _context.SaveChangesAsync();
            HttpContext.Session.SetString("user", JsonConvert.SerializeObject(Customers));
            return RedirectToAction("Information");
        }


        public IActionResult Information()
        {
            var user = JsonConvert.DeserializeObject<Customers>(HttpContext.Session.GetString("user"));
            ViewData["Login"] = true;
            return View(user);
        }

        public IActionResult Logout()
        {
            ViewData["Login"] = null;
            HttpContext.Session.Clear();
            return View("Index");

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
