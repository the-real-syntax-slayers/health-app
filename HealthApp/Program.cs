using Microsoft.EntityFrameworkCore;
using HealthApp.Models;
using HealthApp.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BookingDbContext>(options =>
{
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:BookingDbContextConnection"]);
});

builder.Services.AddScoped<IBookingRepository, BookingRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.MapDefaultControllerRoute();

// app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();