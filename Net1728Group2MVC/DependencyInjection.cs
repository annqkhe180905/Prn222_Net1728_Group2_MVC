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
<<<<<<< HEAD
<<<<<<< HEAD
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
=======
            services.AddScoped<INewsArticleRepository, NewsArticleRepository>();
            services.AddScoped<INewsArticleService, NewsArticleService>();
>>>>>>> fd28fc766bfba8a739aef93536c2f82d6ba65ab6
=======
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<INewsArticleRepository, NewsArticleRepository>();
            services.AddScoped<INewsArticleService, NewsArticleService>();
            services.AddScoped<IReportService, ReportService>();
>>>>>>> 74170a3c1ecedcc38c4ec05d551f5babc8fa12c0
            services.AddSingleton<AdminAccount>();
        }
    }
}
