using kuafor.mvc.Context;
using Microsoft.AspNetCore.Identity;

namespace kuafor.mvc.Services.Extensions
{
    public static class ApplicationExtension
    {
        
        public static void ConfigureLocalization(this WebApplication app)
        {
            app.UseRequestLocalization(options =>
            {
                options.AddSupportedCultures("tr-TR")
                    .AddSupportedUICultures("tr-TR")
                    .SetDefaultCulture("tr-TR");
            });
        }

        public static async void ConfigureDefaultAdminUser(this IApplicationBuilder app)
        {
            const string adminEmail = "g201210077@sakarya.edu.tr";
            const string adminPassword = "sau";

            //UserMAnager
            UserManager<IdentityUser> userManager = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            //RoleManager
            RoleManager<IdentityRole> roleManager = app
                .ApplicationServices
                .CreateAsyncScope()
                .ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            IdentityUser user = await userManager.FindByEmailAsync(adminEmail);
            if (user is null)
            {
                user = new IdentityUser()
                {
                    Email = "g201210077@sakarya.edu.tr",
                    PhoneNumber = "5555555555",
                    UserName = "Admin"
                };

                var result = await userManager.CreateAsync(user, adminPassword);
                if (!result.Succeeded)
                    throw new Exception("Admin user could not created.");

                var roleResult = await userManager.AddToRolesAsync(user,
                    roleManager
                        .Roles
                        .Select(r => r.Name)
                        .ToList()
                );

                if (!roleResult.Succeeded)
                    throw new Exception("System have problems with role defination for admin");
            }

        }

    }
}
