using TCIPlaba1;
using Microsoft.EntityFrameworkCore;
using TCIPlaba1.Models;

//PM> Scaffold-DbContext "Server= LAPTOP-J5R1H9E6; Database=ICTPLaba1; Trusted_Connection=True; Trust Server Certificate=True; " Microsoft.EntityFrameworkCore.SqlServer -f

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Ictplaba1Context>(option => option.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Participants}/{action=Index}/{id?}");

app.Run();
