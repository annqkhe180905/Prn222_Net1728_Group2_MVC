using BLL.Interfaces;
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;

namespace Net1728Group2MVC
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<INewsArticleRepository, NewsArticleRepository>();
            services.AddScoped<INewsArticleService, NewsArticleService>();
            services.AddSingleton<AdminAccount>();
        }
    }
}
