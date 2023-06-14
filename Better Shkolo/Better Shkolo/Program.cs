namespace Better_Shkolo
{
    using Better_Shkolo.Data;
    using Better_Shkolo.Data.Models;
    using Better_Shkolo.Services.AbsenceService;
    using Better_Shkolo.Services.AccountService;
    using Better_Shkolo.Services.DirectorService;
    using Better_Shkolo.Services.GradeService;
    using Better_Shkolo.Services.MarkService;
    using Better_Shkolo.Services.ReviewService;
    using Better_Shkolo.Services.SchoolService;
    using Better_Shkolo.Services.StatisticsService;
    using Better_Shkolo.Services.StudentService;
    using Better_Shkolo.Services.SubjectService;
    using Better_Shkolo.Services.TeacherService;
    using Better_Shkolo.Services.TestService;
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
                context.User.IsInRole("Parent") || context.User.IsInRole("Student")));

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
                context.User.IsInRole("Teacher")));

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

            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddTransient<ISchoolService, SchoolService>();
            builder.Services.AddTransient<IGradeService, GradeService>();
            builder.Services.AddTransient<ITeacherService, TeacherService>();
            builder.Services.AddTransient<ISubjectService, SubjectService>();
            builder.Services.AddTransient<IAbsenceService, AbsencesService>();
            builder.Services.AddTransient<IStudentService, StudentService>();
            builder.Services.AddTransient<IMarkService, MarkService>();
            builder.Services.AddTransient<ITestService, TestService>();
            builder.Services.AddTransient<IDirectorService, DirectorService>();
            builder.Services.AddTransient<IReviewService, ReviewService>();
            builder.Services.AddTransient<IStatisticsService, StatisticsService>();

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