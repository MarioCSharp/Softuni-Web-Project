﻿using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Absence;
using Better_Shkolo.Models.Parent;
using Better_Shkolo.Models.Student;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private ApplicationDbContext context;
        private UserManager<User> userManager;
        public StudentService(ApplicationDbContext context,
                              UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<bool> Add(StudentCreateModel model)
        {
            var count = await context.Students.CountAsync();

            var student = new Student()
            {
                UserId = model.UserId,
                SchoolId = model.SchoolId,
                GradeId = model.GradeId,
                GradeTeacherId = context.Grades.Find(model.GradeId).TeacherId,
            };

            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();

            await userManager.AddToRoleAsync(await context.Users.FindAsync(student.UserId), "Student");

            return count + 1 == await context.Students.CountAsync();
        }

        public async Task<bool> AsignParent(ParentCreateModel model, int id)
        {
            var count = await context.Parents.CountAsync();

            var parent = new Parent()
            {
                UserId = model.UserId,
                StudentId = id
            };

            await context.Parents.AddAsync(parent);
            await context.SaveChangesAsync();

            var user = await context.Users.FindAsync(parent.UserId);

            await userManager.AddToRoleAsync(user, "Parent");

            var student = await context.Students.FindAsync(id);
            student.ParentId = parent.Id;
            await context.SaveChangesAsync();

            return count + 1 == await context.Parents.CountAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var count = await context.Students.CountAsync();

            var student = await context.Students.FindAsync(id);
            var studentUser = await context.Users.FindAsync(student.UserId);

            var parents = await context.Parents.Where(x => x.StudentId == id).ToListAsync();

            foreach (var parent in parents)
            {
                context.Parents.Remove(parent);
                await context.SaveChangesAsync();

                var user = await context.Users.FindAsync(parent.UserId);

                await userManager.RemoveFromRoleAsync(user, "Parent");
            }

            await userManager.RemoveFromRoleAsync(studentUser, "Student");

            context.Students.Remove(student);
            await context.SaveChangesAsync();

            return count - 1 == await context.Students.CountAsync();
        }

        public async Task<bool> Edit(StudentCreateModel model, int id)
        {
            var student = await GetStudent(id);

            if (student is null)
            {
                return false;
            }

            student.GradeId = model.GradeId;

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<Student> GetStudent(int id)
        {
            return await context.Students.FindAsync(id);
        }

        public async Task<AbsencesAddModel> GetStudentModel(int id)
        {
            var student = await context.Students.FindAsync(id);
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == student.UserId);

            var model = new AbsencesAddModel();
            
            model.Id = id;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.SchoolId = student.SchoolId;

            return model;
        }

        public async Task<List<StudentDisplayModel>> GetStudentsInSchool(int id)
        {
            return await context.Students.Where(x => x.SchoolId == id)
                .Select(x => new StudentDisplayModel
                {
                    Id = x.Id,
                    FirstName = context.Users.FirstOrDefault(y => y.Id == x.UserId).FirstName,
                    LastName = context.Users.FirstOrDefault(y => y.Id == x.UserId).LastName,
                    Email = context.Users.FirstOrDefault(y => y.Id == x.UserId).Email,
                    SchoolId = x.SchoolId,
                    SchoolName = context.Schools.FirstOrDefault(y => y.Id == x.SchoolId).Name,
                }).ToListAsync();
        }

        public async Task<List<StudentDisplayModel>> GetStudentsInSubject(int id)
        {
            var subject = await context.Subjects.FindAsync(id);

            return await context.Students.Where(x => x.GradeId == subject.GradeId)
                .Select(x => new StudentDisplayModel()
                {
                    Id = x.Id,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Email = x.User.Email,
                    SchoolId = x.SchoolId,
                    SchoolName = x.School.Name,
                }).ToListAsync();
        }
    }
}
