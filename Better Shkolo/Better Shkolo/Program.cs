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

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("CanAddAbsenceses", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Teacher")));

                options.AddPolicy("CanViewAbsencesesForStudent", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Student") || context.User.IsInRole("Parent")));

                options.AddPolicy("CanAccessAdminMenu", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Administrator")));

                options.AddPolicy("CanAddGrades", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Administrator") || context.User.IsInRole("Director")));

                options.AddPolicy("CanDeleteGrades", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Administrator") || context.User.IsInRole("Director")));

                options.AddPolicy("CanEditGrades", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Administrator") || context.User.IsInRole("Director")));

                options.AddPolicy("CanViewGrades", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Administrator") || context.User.IsInRole("Director")));

                options.AddPolicy("CanAddMarks", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Teacher")));

                options.AddPolicy("CanViewMarks", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Parent") || context.User.IsInRole("Student")));

                options.AddPolicy("CanAddReviews", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Teacher")));

                options.AddPolicy("CanViewReviews", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Parent") || context.User.IsInRole("Student") || context.User.IsInRole("Teacher")));

                options.AddPolicy("CanEditSchools", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Director") || context.User.IsInRole("Administrator")));

                options.AddPolicy("CanAccessSchools", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Administrator")));

                options.AddPolicy("CanAccessStudents", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Administrator") || context.User.IsInRole("Director")));

                options.AddPolicy("CanDisplayStudentsInSubject", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Teacher") || context.User.IsInRole("Director")
                || context.User.IsInRole("Administrator")));

                options.AddPolicy("CanAddSubject", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Director") || context.User.IsInRole("Administrator")));

                options.AddPolicy("CanViewSubjects", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Director") || context.User.IsInRole("Administrator")));

                options.AddPolicy("CanManageSubjects", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Teacher")));

                options.AddPolicy("CanEditSubjects", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Director") || context.User.IsInRole("Administrator")));

                options.AddPolicy("CanDeleteSubjects", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Director") || context.User.IsInRole("Administrator")));

                options.AddPolicy("CanAccessTeachers", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Director") || context.User.IsInRole("Administrator")));

                options.AddPolicy("CanAddTests", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Teacher")));

                options.AddPolicy("CanViewTests", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Parent") || context.User.IsInRole("Student")));

                options.AddPolicy("CanAccessDirectorMenu", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Director")));
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