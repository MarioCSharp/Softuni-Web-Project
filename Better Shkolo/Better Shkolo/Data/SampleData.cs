using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BetterShkolo.Data.Models;

namespace BetterShkolo.Data
{
    public static class SampleData
    {
        public static async void Initialize(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            SeedAdministrator(services);
            MigrateDatabase(services);
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();
            data.Database.Migrate();
        }

        public static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            Task
                .Run(async () =>
                {
                    if (!await roleManager.RoleExistsAsync("Parent"))
                    {
                        var parent = new IdentityRole("Parent");

                        await roleManager.CreateAsync(parent);
                    }
                    if (await roleManager.RoleExistsAsync("Administrator"))
                    {
                        return;
                    }

                    var admin = new IdentityRole("Administrator");
                    var director = new IdentityRole("Director");
                    var teacher = new IdentityRole("Teacher");
                    var student = new IdentityRole("Student");

                    await roleManager.CreateAsync(admin);
                    await roleManager.CreateAsync(director);
                    await roleManager.CreateAsync(teacher);
                    await roleManager.CreateAsync(student);

                    const string adminEmail = "mario_petkov2007@abv.bg";
                    const string adminPassword = "Administrator?123";

                    var user = new User
                    {
                        Email = adminEmail,
                        FirstName = "Mario",
                        LastName = "Petkov",
                        UserName = adminEmail,
                        EmailConfirmed = true,
                        Address = "",
                        City = "",
                        Country = "",
                        Phone = "",
                        Chronic = "",
                        DoctorAddress = "",
                        DoctorName = "",
                        DoctorPhone = ""
                    };

                    await userManager.CreateAsync(user, adminPassword);
                    await userManager.AddToRoleAsync(user, admin.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
