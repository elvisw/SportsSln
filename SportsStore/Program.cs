using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StoreDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("SportsStoreConnection")));
builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("catpage", "{category}/Page{productPage:int}",
    new { Controller = "Home", action = "Index" });

    endpoints.MapControllerRoute("page", "Page{productPage:int}",
    new { Controller = "Home", action = "Index", productPage = 1 });

    endpoints.MapControllerRoute("category", "{category}",
    new { Controller = "Home", action = "Index", productPage = 1 });

    endpoints.MapControllerRoute("pagination", "Products/Page{productPage}",
    new { Controller = "Home", action = "Index", productPage = 1 });

    endpoints.MapDefaultControllerRoute();
});
//app.MapGet("/", () => "Hello World!");
SeedData.EnsurePopulated(app);

app.Run();