using Microsoft.EntityFrameworkCore;
using TCIPlaba1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using DocumentFormat.OpenXml.Spreadsheet;

internal class Program
{
	private static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllersWithViews();

		var a = builder.Configuration.GetConnectionString("DefaultConnection");
		builder.Services.AddDbContext<Ictplaba1Context>(option => option.UseSqlServer(a));

		var b = builder.Configuration.GetConnectionString("IdentityConnection");
		builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlServer(b));

		builder.Services.AddControllersWithViews();
		builder.Services.AddIdentity<TCIPlaba1.Models.Users, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();


		var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var userManager = services.GetRequiredService<UserManager<TCIPlaba1.Models.Users>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await RoleInitializer.InitializeAsync(userManager, roleManager);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, $"An error occurred while seeding the database. {DateTime.Now}");
            }
        }

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
		app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
			//pattern: "{controller=Participants}/{action=Index}/{id?}");
			pattern: "{controller=Participants}/{action=Index}/{id?}");

		app.Run();
	}
}