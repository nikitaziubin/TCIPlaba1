using Microsoft.EntityFrameworkCore;
using TCIPlaba1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;




//PM> Scaffold-DbContext "Server= LAPTOP-J5R1H9E6; Database=ICTPLaba1; Trusted_Connection=True; Trust Server Certificate=True; " Microsoft.EntityFrameworkCore.SqlServer -f

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllersWithViews();

		var a = builder.Configuration.GetConnectionString("DefaultConnection");
		builder.Services.AddDbContext<Ictplaba1Context>(option => option.UseSqlServer(a));

		var b = builder.Configuration.GetConnectionString("IdentityConnection");
		builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(b));

		builder.Services.AddControllersWithViews();
		builder.Services.AddIdentity<Users, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();


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

		app.UseAuthentication();

		app.UseRouting();


		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Participants}/{action=Index}/{id?}");

		app.Run();
	}
}