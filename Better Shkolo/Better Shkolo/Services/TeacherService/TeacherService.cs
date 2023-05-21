﻿using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Teacher;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.TeacherService
{
    public class TeacherService : ITeacherService
    {
        private ApplicationDbContext context;
        private UserManager<User> userManager;
        public TeacherService(ApplicationDbContext context,
                              UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<bool> Create(TeacherCreateModel model)
        {
            var countNow = await context.Teachers.CountAsync();

            var teacher = new Teacher()
            {
                SchoolId = model.SchoolId,
                UserId = model.UserId
            };

            await context.Teachers.AddAsync(teacher);
            await context.SaveChangesAsync();

            var user = await context.Users.FindAsync(model.UserId);

            if (user != null)
            {
                await userManager.AddToRoleAsync(user, "Teacher");
            }
            else
            {
                return false;
            }

            return countNow + 1 == await context.Teachers.CountAsync();
        }

        public async Task<bool> DeleteTeacher(int id)
        {
            var count = await context.Teachers.CountAsync();
            var teacher = await context.Teachers.FindAsync(id);

            if (teacher is null)
            {
                return false;
            }

            context.Teachers.Remove(teacher);
            await context.SaveChangesAsync();

            await userManager
                .RemoveFromRoleAsync(context.Users.FirstOrDefault(x => x.Id == teacher.UserId), "Teacher");

            return count - 1 == await context.Teachers.CountAsync();
        }

        public async Task<List<TeacherDisplayModel>> GetAllTeacherInSchool(int schoolId)
        {
            var result = await context.Teachers.Where(x => x.SchoolId == schoolId)
                .Select(x => new TeacherDisplayModel
                {
                    Id = x.Id,
                    FirstName = context.Users.FirstOrDefault(y => y.Id == x.UserId).FirstName,
                    LastName = context.Users.FirstOrDefault(y => y.Id == x.UserId).LastName,
                    Email = context.Users.FirstOrDefault(y => y.Id == x.UserId).Email,
                    SchoolId = schoolId
                }).ToListAsync();

            return result;
        }

        public async Task<Teacher> GetTeacher(int id)
        {
            return await context.Teachers.FindAsync(id);
        }
    }
}
