using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using Net1728Group2MVC.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Net1728Group2MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        public ActionResult Login()
        {
            if (HttpContext.Session.GetString("IsAuthenticated") == "True")
            {
                return RedirectToAction("Index", "Home"); 
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountService.Login(loginModel.email, loginModel.password);

                if (user != null)
                {
                    HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));
                    HttpContext.Session.SetString("IsAuthenticated", "True");

                    return RedirectToAction("Index", "Home");
                }

                ViewBag.ErrorLoginMessage = "Invalid email or password!";
                
            }

            return View(loginModel);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

    }
}
