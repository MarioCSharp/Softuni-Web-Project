﻿using Better_Shkolo.Data.Models;
using BetterShkolo.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BetterShkolo.Data
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
        public DbSet<Test> Tests { get; set; }
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
        public DbSet<Post> Posts { get; set; }
        public DbSet<ErasmusDocument> ErasmusDocuments { get; set; }
        public DbSet<ErasmusApplication> ErasmusApplications { get; set; }
        public DbSet<Diploma> Diplomas { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
    }
}