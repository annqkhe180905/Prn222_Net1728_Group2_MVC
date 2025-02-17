using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Net1728Group2MVC.Models;

namespace Net1728Group2MVC.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (HttpContext.Items["User"] is AccountModel user)
            {
                ViewBag.UserName = user.AccountName;
                ViewBag.UserEmail = user.AccountEmail;
                ViewBag.Role = user.AccountRole;
            }
        }
    }
}
