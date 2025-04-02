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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.MapStaticAssets();

app.MapControllerRoute(name: "default", pattern: "{controller=Party}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
