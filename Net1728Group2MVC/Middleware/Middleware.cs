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
            if (!string.IsNullOrEmpty(userJson))
            {
                var user = JsonConvert.DeserializeObject<AccountModel>(userJson);
                context.Items["User"] = user; 
            }

            var allowedRoutes = new[]
            {
                "/Auth/Login",
                "/Home/Index"
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

            await _next(context);
        }
    }
}
