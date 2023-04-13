using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Identity;
namespace TCIPlaba1.Models
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "Qwerty_1";

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                Users admin = new Users { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
