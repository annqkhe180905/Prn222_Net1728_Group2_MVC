using Net1728Group2MVC.Models;
using Newtonsoft.Json;

namespace Net1728Group2MVC.Middleware
{
    public class Middleware
    {
        private readonly RequestDelegate _next;

        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var userJson = context.Session.GetString("User");
            AccountModel user = null;

            if (!string.IsNullOrEmpty(userJson))
            {
                user = JsonConvert.DeserializeObject<AccountModel>(userJson);
                context.Items["User"] = user; 
            }

            var allowedRoutes = new[]
            {
                "/Auth/Login",
                "/Home/Index",
                "/Home/Detail"
            };

            bool isAllowedRoute = false;
            foreach (var route in allowedRoutes)
            {
                if (context.Request.Path.StartsWithSegments(route))
                {
                    isAllowedRoute = true;
                    break;
                }
            }
            if (context.Items["User"] == null && !isAllowedRoute)
            {
                context.Response.Redirect("/Auth/Login");
                return;
            }

            var roleBasedRoutes = new Dictionary<int?, string[]>
            {
                { 0, new[] { "/Admin", "/Admin/Account", "/Admin/Report", "/Auth/Logout" } }, 
                { 1, new[] { "/Staff", "/Staff/Category", "/Staff/Profile", "/Staff/News", "/Staff/NewsTag", "/Auth/Logout", 
                    "/NewsArticle", "/NewsTag", "/Category", } }, 
                { 2, new[] { "/Lecturer", "/Lecturer/News", "/Auth/Logout" } }

            };

            if (user != null && roleBasedRoutes.ContainsKey(user.AccountRole))
            {
                bool hasAccess = false;
                foreach (var route in roleBasedRoutes[user.AccountRole])
                {
                    if (context.Request.Path.StartsWithSegments(route))
                    {
                        hasAccess = true;
                        break;
                    }
                }

                if (!hasAccess && !isAllowedRoute)
                {
                    string url = user.AccountRole switch
                    {
                        0 => "/Admin",
                        1 => "/Staff",
                        2 => "/Lecturer",
                    };
                    context.Response.Redirect(url);
                    return;
                }
            }

            await _next(context);
        }
    }
}
