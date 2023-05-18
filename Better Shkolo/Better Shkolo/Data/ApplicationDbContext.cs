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
            modelBuilder.Entity<Absences>(entity =>
            {
                entity.HasOne(a => a.Subject)
                    .WithMany()
                    .HasForeignKey(a => a.SubjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(a => a.Teacher)
                    .WithMany()
                    .HasForeignKey(a => a.TeacherId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Student)
                    .WithMany(s => s.Absences)
                    .HasForeignKey(a => a.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(a => a.School)
                    .WithMany()
                    .HasForeignKey(a => a.SchoolId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Grade
            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasOne(g => g.Teacher)
                    .WithMany()
                    .HasForeignKey(g => g.TeacherId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(g => g.School)
                    .WithMany(s => s.Grades)
                    .HasForeignKey(g => g.SchoolId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Mark
            modelBuilder.Entity<Mark>(entity =>
            {
                entity.HasOne(m => m.Subject)
                    .WithMany()
                    .HasForeignKey(m => m.SubjectId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Teacher)
                    .WithMany()
                    .HasForeignKey(m => m.TeacherId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(m => m.Student)
                    .WithMany(s => s.Marks)
                    .HasForeignKey(m => m.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(m => m.School)
                    .WithMany()
                    .HasForeignKey(m => m.SchoolId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Review
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasOne(r => r.Subject)
                    .WithMany()
                    .HasForeignKey(r => r.SubjectId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Teacher)
                    .WithMany()
                    .HasForeignKey(r => r.TeacherId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.Student)
                    .WithMany(s => s.Reviews)
                    .HasForeignKey(r => r.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.School)
                    .WithMany()
                    .HasForeignKey(r => r.SchoolId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // School
            modelBuilder.Entity<School>(entity =>
            {
                entity.HasOne(s => s.Director)
                    .WithMany()
                    .HasForeignKey(s => s.DirectorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Student
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasOne(s => s.User)
                    .WithOne()
                    .HasForeignKey<Student>(s => s.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.School)
                    .WithMany(school => school.Students)
                    .HasForeignKey(s => s.SchoolId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Grade)
                    .WithMany(g => g.Students)
                    .HasForeignKey(s => s.GradeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.GradeTeacher)
                    .WithMany()
                    .HasForeignKey(s => s.GradeTeacherId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Teacher
            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasOne(t => t.School)
                    .WithMany(s => s.Teachers)
                    .HasForeignKey(t => t.SchoolId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.User)
                    .WithOne()
                    .HasForeignKey<Teacher>(t => t.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Subject
            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasOne(s => s.Teacher)
                    .WithMany()
                    .HasForeignKey(s => s.TeacherId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.School)
                    .WithMany()
                    .HasForeignKey(s => s.SchoolId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Grade)
                    .WithMany()
                    .HasForeignKey(s => s.GradeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Test
            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasOne(t => t.Subject)
                    .WithMany()
                    .HasForeignKey(t => t.SubjectId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Teacher)
                    .WithMany()
                    .HasForeignKey(t => t.TeacherId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Grade)
                    .WithMany()
                    .HasForeignKey(t => t.GradeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.School)
                    .WithMany()
                    .HasForeignKey(t => t.SchoolId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}