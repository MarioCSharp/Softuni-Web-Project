using AutoMapper;
using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Grade;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.GradeService
{
    public class GradeService : IGradeService
    {
        private ApplicationDbContext context;
        private UserManager<User> userManager; 
        private IMapper mapper;
        public GradeService(ApplicationDbContext context,
                            UserManager<User> userManager,
                            IMapper mapper)
        {
            this.context = context;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public async Task<bool> Create(GradeCreateModel model)
        {
            if (string.IsNullOrEmpty(model.GradeName) || string.IsNullOrEmpty(model.GradeSpecialty)
                || !await context.Schools.AnyAsync(x => x.Id == model.SchoolId)
                || !await context.Teachers.AnyAsync(x => x.Id == model.TeacherId))
            {
                return false;
            }

            var grade = mapper.Map<Grade>(model);

            await context.Grades.AddAsync(grade);
            await context.SaveChangesAsync();

            return await context.Grades.ContainsAsync(grade);
        }

        public async Task<bool> DeleteGrade(int id)
        {
            var grade = await context.Grades.FindAsync(id);

            if (grade == null)
            {
                return false;
            }

            foreach (var parent in context.Parents.ToArray())
            {
                var student = await context.Students.FindAsync(parent.StudentId);

                if (student.GradeId != grade.Id)
                {
                    continue;
                }

                context.Parents.Remove(parent);
                await context.SaveChangesAsync();

                await userManager.RemoveFromRoleAsync(await context.Users.FindAsync(parent.UserId), "Parent");
            }

            var studentsToDelete = await context.Students.Where(x => x.GradeId == grade.Id).ToArrayAsync();

            foreach (var student in studentsToDelete)
            {
                await userManager.RemoveFromRoleAsync(await context.Users.FindAsync(student.UserId), "Student");
            }

            context.Subjects.RemoveRange(await context.Subjects.Where(x => x.GradeId == grade.Id).ToArrayAsync());
            context.Students.RemoveRange(studentsToDelete);
            context.Tests.RemoveRange(await context.Tests.Where(x => x.GradeId == grade.Id).ToArrayAsync());
            

            context.Grades.Remove(grade);

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<Grade> GetGrade(int id)
        {
            return await context.Grades.FindAsync(id);
        }

        public async Task<Grade> GetGradeByTeacherId(int teacherId)
        {
            return await context.Grades.FirstOrDefaultAsync(x => x.TeacherId == teacherId);
        }

        public async Task<List<GradeDisplayModel>> GetGradesBySchoolId(int schoolId)
        {
            return await context.Grades.Where(x => x.SchoolId == schoolId)
                .Select(x => new GradeDisplayModel()
                {
                    Id = x.Id,
                    GradeName = x.GradeName
                }).ToListAsync();
        }
    }
}
