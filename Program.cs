using Microsoft.EntityFrameworkCore;
using NJUPT_AspNetCore_Exp2.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Database"));
});

var app = builder.Build();

app.UseRouting();

app.MapStaticAssets();

app.MapControllerRoute(name: "default", pattern: "{controller=Party}/{action=Index}/{id?}");

app.Run();
