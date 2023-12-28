using BAL.Interface;
using BAL.Services;
using DAL.DbContexts;
using DAL.Interface;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using RentalCarsandBikes.Models;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var com = builder.Configuration.GetConnectionString("Con");
builder.Services.AddDbContext<RentalSystemContext>(m=>m.UseSqlServer(com));
builder.Services.AddScoped<BAL_Interface, BAL_Services>();
builder.Services.AddScoped<DAL_Interface, DAL_Repositories>();



//builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
