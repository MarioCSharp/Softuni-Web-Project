namespace Better_Shkolo
{
    using Better_Shkolo.Data;
    using Better_Shkolo.Data.Models;
    using Better_Shkolo.Services.AbsenceService;
    using Better_Shkolo.Services.AccountService;
    using Better_Shkolo.Services.GradeService;
    using Better_Shkolo.Services.MarkService;
    using Better_Shkolo.Services.ReviewService;
    using Better_Shkolo.Services.SchoolService;
    using Better_Shkolo.Services.StudentService;
    using Better_Shkolo.Services.SubjectService;
    using Better_Shkolo.Services.TeacherService;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
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

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("CanViewSubjects", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Teacher") || context.User.IsInRole("Student")
                || context.User.IsInRole("Parent") || context.User.IsInRole("Administrator")
                || context.User.IsInRole("Director")));

                options.AddPolicy("CanEditDeleteAndCreateSubjects", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Director") || context.User.IsInRole("Administrator")));

                options.AddPolicy("CanAccessTeachers", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Director") || context.User.IsInRole("Administrator")));

                options.AddPolicy("CanAccessStudents", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Director") || context.User.IsInRole("Administrator")));

                options.AddPolicy("CanAccessSchools", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Administrator")));

                options.AddPolicy("CanEditDeleteAndCreateGrades", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Director") || context.User.IsInRole("Administrator")));

                options.AddPolicy("CanViewGrades", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Director") || context.User.IsInRole("Administrator")
                || context.User.IsInRole("Teacher")));

                options.AddPolicy("CanAccessAdminMenu", policy => policy
                .RequireAssertion(context =>
                context.User.IsInRole("Administrator")));
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });

            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddTransient<ISchoolService, SchoolService>();
            builder.Services.AddTransient<IGradeService, GradeService>();
            builder.Services.AddTransient<ITeacherService, TeacherService>();
            builder.Services.AddTransient<ISubjectService, SubjectService>();
            builder.Services.AddTransient<IAbsenceService, AbsencesService>();
            builder.Services.AddTransient<IStudentService, StudentService>();
            builder.Services.AddTransient<IMarkService, MarkService>();
            builder.Services.AddTransient<IReviewService, ReviewService>();

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