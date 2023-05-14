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
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Absences> Absencess { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User
            modelBuilder.Entity<User>()
                .HasMany(u => u.Marks)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Аbsences)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.School)
                .WithMany()
                .HasForeignKey(u => u.SchoolId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Grade)
                .WithMany()
                .HasForeignKey(u => u.GradeId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.GradeTeacher)
                .WithMany()
                .HasForeignKey(u => u.GradeTeacherId);

            //Grade

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Teacher)
                .WithMany()
                .HasForeignKey(g => g.TeacherId);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.School)
                .WithMany()
                .HasForeignKey(g => g.SchoolId);

            //Mark

            modelBuilder.Entity<Mark>()
                .HasOne(m => m.Subject)
                .WithMany()
                .HasForeignKey(m => m.SubjectId);

            modelBuilder.Entity<Mark>()
                .HasOne(m => m.Teacher)
                .WithMany()
                .HasForeignKey(m => m.TeacherId);

            modelBuilder.Entity<Mark>()
                .HasOne(m => m.User)
                .WithMany(u => u.Marks)
                .HasForeignKey(m => m.UserId);

            modelBuilder.Entity<Mark>()
                .HasOne(m => m.School)
                .WithMany()
                .HasForeignKey(m => m.SchoolId);

            //Review

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Subject)
                .WithMany()
                .HasForeignKey(r => r.SubjectId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Teacher)
                .WithMany()
                .HasForeignKey(r => r.TeacherId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.School)
                .WithMany()
                .HasForeignKey(r => r.SchoolId);

            //School

            modelBuilder.Entity<School>()
                .HasMany(s => s.Grades)
                .WithOne(g => g.School)
                .HasForeignKey(g => g.SchoolId);

            modelBuilder.Entity<School>()
                .HasMany(s => s.Teachers)
                .WithOne(t => t.School)
                .HasForeignKey(t => t.SchoolId);

            modelBuilder.Entity<School>()
                .HasMany(s => s.Users)
                .WithOne(u => u.School)
                .HasForeignKey(u => u.SchoolId);

            //Subject

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Teacher)
                .WithMany()
                .HasForeignKey(s => s.TeacherId);

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.School)
                .WithMany()
                .HasForeignKey(s => s.SchoolId);

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.Grade)
                .WithMany()
                .HasForeignKey(s => s.GradeId);

            //Teacher

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Subject)
                .WithMany()
                .HasForeignKey(t => t.SubjectId);

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.School)
                .WithMany()
                .HasForeignKey(t => t.SchoolId);

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.User)
                .WithOne(u => u.GradeTeacher)
                .HasForeignKey<Teacher>(t => t.UserId);

            //Test

            modelBuilder.Entity<Test>()
                .HasOne(t => t.Subject)
                .WithMany()
                .HasForeignKey(t => t.SubjectId);

            modelBuilder.Entity<Test>()
                .HasOne(t => t.Teacher)
                .WithMany()
                .HasForeignKey(t => t.TeacherId);

            modelBuilder.Entity<Test>()
                .HasOne(t => t.Grade)
                .WithMany()
                .HasForeignKey(t => t.GradeId);

            modelBuilder.Entity<Test>()
                .HasOne(t => t.School)
                .WithMany()
                .HasForeignKey(t => t.SchoolId);

            //Аbsence

            modelBuilder.Entity<Absences>()
                .HasOne(a => a.Subject)
                .WithMany()
                .HasForeignKey(a => a.SubjectId);

            modelBuilder.Entity<Absences>()
                .HasOne(a => a.Teacher)
                .WithMany()
                .HasForeignKey(a => a.TeacherId);

            modelBuilder.Entity<Absences>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Absences>()
                .HasOne(a => a.School)
                .WithMany()
                .HasForeignKey(a => a.SchoolId);
            base.OnModelCreating(modelBuilder);
        }
    }
}