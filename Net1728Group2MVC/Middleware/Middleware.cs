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
            await _next(context);
        }
    }
}
