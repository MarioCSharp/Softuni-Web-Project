using Better_Shkolo.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Grade> Grades { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Data.Models.Test> Tests { get; set; }
        public DbSet<Absences> Absencess { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<TermMark> TermMarks { get; set; }
        public DbSet<YearMark> YearMarks { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<StudyPlan> StudyPlans { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Resource> Resources { get; set; }
    }
}