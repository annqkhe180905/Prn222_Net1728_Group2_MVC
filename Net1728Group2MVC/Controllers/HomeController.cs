using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Net1728Group2MVC.Models;
using Newtonsoft.Json;

namespace Net1728Group2MVC.Controllers
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
            var isAuthenticated = HttpContext.Session.GetString("IsAuthenticated");
            var userString = HttpContext.Session.GetString("User");

            if (isAuthenticated == "True")
            {
                AccountModel user = JsonConvert.DeserializeObject<AccountModel>(userString);
                ViewBag.UserName = $"{user.AccountName}";
                ViewBag.UserEmail = $"{user.AccountEmail}";
                ViewBag.Role = $"{user.AccountRole}";
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

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
