using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Grade;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.GradeService
{
    public class GradeService : IGradeService
    {
        private ApplicationDbContext context;
        public GradeService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> Create(GradeCreateModel model)
        {
            var count = await context.Grades.CountAsync();

            var grade = new Grade()
            {
                GradeName = model.GradeName,
                GradeSpecialty = model.GradeSpecialty,
                SchoolId = model.SchoolId,
                TeacherId = model.TeacherId
            };

            await context.Grades.AddAsync(grade);
            await context.SaveChangesAsync();

            if (count + 1 == await context.Grades.CountAsync())
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteGrade(int id)
        {
            var grade = await context.Grades.FindAsync(id);

            if (grade == null)
            {
                return false;
            }

            foreach (var subject in context.Subjects.Where(x => x.GradeId == grade.Id).ToArray())
            {
                context.Subjects.Remove(subject);
            }

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
