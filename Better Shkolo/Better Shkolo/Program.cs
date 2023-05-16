using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Services.AccountService;
using Better_Shkolo.Services.GradeService;
using Better_Shkolo.Services.SchoolService;
using Better_Shkolo.Services.TeacherService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
              .AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();

            builder.Services.AddControllers(
                options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });

            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddTransient<ISchoolService, SchoolService>();
            builder.Services.AddTransient<IGradeService, GradeService>();
            builder.Services.AddTransient<ITeacherService, TeacherService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Initialize();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}