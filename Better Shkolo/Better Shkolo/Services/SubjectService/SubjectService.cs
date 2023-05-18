using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Subject;

namespace Better_Shkolo.Services.SubjectService
{
    public class SubjectService : ISubjectService
    {
        private ApplicationDbContext context;
        public SubjectService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> Create(SubjectCreateModel model)
        {
            var count = context.Subjects.Count();

            var subject = new Subject()
            {
                Name = model.Name,
                TeacherId = model.TeacherId,
                SchoolId = model.SchoolId,
                GradeId = model.GradeId
            };

            await context.Subjects.AddAsync(subject);
            await context.SaveChangesAsync();

            return count + 1 == context.Subjects.Count();
        }
    }
}
