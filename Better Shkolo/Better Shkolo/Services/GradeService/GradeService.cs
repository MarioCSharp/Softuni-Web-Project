using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Grade;

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
            var count = context.Grades.Count();

            var grade = new Grade()
            {
                GradeName = model.GradeName,
                GradeSpecialty = model.GradeSpecialty,
                SchoolId = model.SchoolId,
                TeacherId = model.TeacherId
            };

            await context.Grades.AddAsync(grade);
            await context.SaveChangesAsync();

            if (count + 1 == context.Grades.Count())
            {
                return true;
            }

            return false;
        }

        public bool DeleteGrade(int id)
        {
            var grade = context.Grades.Find(id);

            if (grade == null)
            {
                return false;
            }

            foreach (var subject in context.Subjects.Where(x => x.GradeId == grade.Id).ToArray())
            {
                context.Subjects.Remove(subject);
            }

            context.Grades.Remove(grade);

            context.SaveChanges();

            return true;
        }

        public Grade GetGrade(int id)
        {
            return context.Grades.FirstOrDefault(x => x.Id == id);
        }

        public List<GradeDisplayModel> GetGradesBySchoolId(int schoolId)
        {
            return context.Grades.Where(x => x.SchoolId == schoolId)
                .Select(x => new GradeDisplayModel()
                {
                    Id = x.Id,
                    GradeName = x.GradeName
                }).ToList();
        }
    }
}
