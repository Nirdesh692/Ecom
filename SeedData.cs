using Ecom.Models;
using Microsoft.AspNetCore.Identity;


public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

        // Create roles
        string[] roleNames = { "Admin", "Customer" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole { Name = roleName });
            }
        }

        // Create admin user
        var adminUser = new User
        {
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com",
            EmailConfirmed = true
        };

        if (await userManager.FindByNameAsync(adminUser.UserName) == null)
        {
            var result = await userManager.CreateAsync(adminUser, "Admin@123");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
