namespace Better_Shkolo
{
    using Better_Shkolo.Data;
    using Better_Shkolo.Data.Models;
    using Better_Shkolo.Extensions;
    using Better_Shkolo.Services.AccountService;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                Args = args,
                ApplicationName = typeof(Program).Assembly.FullName
            });

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

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            });

            builder.Services.AddAuthorization(policy =>
            {
                policy.AddPolicy("AdministratorPolicy", options =>
                {
                    options.RequireAuthenticatedUser();
                    options.RequireRole("Administrator");
                });

                policy.AddPolicy("DirectorPolicy", options =>
                {
                    options.RequireAuthenticatedUser();
                    options.RequireRole("Director");
                });

                policy.AddPolicy("AdministratorDirectorPolicy", options =>
                    options.RequireAssertion(context =>
                        context.User.IsInRole("Administrator") || context.User.IsInRole("Director")));

                policy.AddPolicy("AdministratorDirectorTeacherPolicy", options =>
                    options.RequireAssertion(context =>
                        context.User.IsInRole("Administrator") || context.User.IsInRole("Director")
                        || context.User.IsInRole("Teacher")));

                policy.AddPolicy("DirectorTeacherPolicy", options =>
                    options.RequireAssertion(context =>
                         context.User.IsInRole("Director")
                        || context.User.IsInRole("Teacher")));

                policy.AddPolicy("TeacherPolicy", options =>
                {
                    options.RequireAuthenticatedUser();
                    options.RequireRole("Teacher");
                });

                policy.AddPolicy("StudentPolicy", options =>
                {
                    options.RequireAuthenticatedUser();
                    options.RequireRole("Student");
                });

                policy.AddPolicy("StudentParentPolicy", options =>
                    options.RequireAssertion(context =>
                        context.User.IsInRole("Student") || context.User.IsInRole("Parent")));

                policy.AddPolicy("StudentParentTeacherPolicy", options =>
                    options.RequireAssertion(context =>
                        context.User.IsInRole("Student") || context.User.IsInRole("Parent") || context.User.IsInRole("Teacher")));

                policy.AddPolicy("AdministratorDirectorTeacherPolicy", options =>
                    options.RequireAssertion(context =>
                        context.User.IsInRole("Administrator") || context.User.IsInRole("Director") || context.User.IsInRole("Teacher")));
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });

            builder.Services.AddApplicationServices(typeof(IAccountService));
            builder.Services.AddMemoryCache();
            builder.Services.AddAutoMapper(typeof(Program));

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

            app.UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting();

            app.UseAuthentication()
                .UseAuthorization()
                .UseStatusCodePages()
                .Initialize();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.MapRazorPages();

            app.Run();
        }
    }
}