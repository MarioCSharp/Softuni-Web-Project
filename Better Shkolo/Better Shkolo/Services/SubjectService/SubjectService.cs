using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Subject;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> DeleteSubject(int id)
        {
            var subject = await context.Subjects.FindAsync(id);

            if (subject == null)
            {
                return false;
            }

            var count = await context.Subjects.CountAsync();

            context.Subjects.Remove(subject);
            await context.SaveChangesAsync();

            return count - 1 == await context.Subjects.CountAsync();
        }

        public Subject GetSubject(int id)
        {
            return context.Subjects.FirstOrDefault(x => x.Id == id);
        }

        public List<SubjectDisplayModel> GetSubjectsBySchoolId(int Id)
        {
            return context.Subjects.Where(x => x.SchoolId == Id).Select(x => new SubjectDisplayModel
            {
                Id = x.Id,
                TeacherId = x.TeacherId,
                Name = x.Name,
            }).ToList();
        }

        public List<SubjectDisplayModel> GetSubjectsByTeacherId(int id)
        {
            var result = context.Subjects.Where(x => x.TeacherId == id)
                    .Select(x => new SubjectDisplayModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        TeacherId = x.TeacherId
                    }).ToList();

            return result;
        }
    }
}
