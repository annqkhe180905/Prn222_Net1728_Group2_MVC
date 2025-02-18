using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Net1728Group2MVC;
using Net1728Group2MVC.Middleware;
using System.Configuration;
using AutoMapper;
using BLL.Mapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MapperConfigure));


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices();
builder.Services.AddDbContext<FunewsManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.Configure<AdminAccount>(builder.Configuration.GetSection("AdminAccount"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<Middleware>();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=login}/{id?}");

app.MapControllerRoute(
    name: "home",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "AccountManagement",
    pattern: "{controller=Admin}/{action=Account}/{id?}");

app.MapControllerRoute(
    name: "ReportManagement",
    pattern: "{controller=Admin}/{action=Report}/{id?}");

app.MapControllerRoute(
    name: "CategoryManagement",
    pattern: "{controller=Staff}/{action=Category}/{id?}");

app.MapControllerRoute(
    name: "NewsManagement",
    pattern: "{controller=Staff}/{action=News}/{id?}");

app.MapControllerRoute(
    name: "ProfileManagement",
    pattern: "{controller=Staff}/{action=Profile}/{id?}");

app.MapControllerRoute(
    name: "ViewNews",
    pattern: "{controller=Lecturer}/{action=News}/{id?}");


app.Run();

