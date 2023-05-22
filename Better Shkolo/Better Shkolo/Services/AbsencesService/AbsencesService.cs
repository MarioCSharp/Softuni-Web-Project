using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Absence;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.AbsenceService
{
    public class AbsencesService : IAbsenceService
    {
        private ApplicationDbContext context;
        public AbsencesService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> Add(AbsencesAddModel model, int id)
        {
            var count = await context.Absencess.CountAsync();
            var subject = await context.Subjects.FindAsync(model.SubjectId);

            if (subject is null)
            {
                return false;
            }

            var teacherId = subject.TeacherId;

            var absence = new Absences()
            {
                AddedOn = DateTime.Now,
                SubjectId = model.SubjectId,
                TeacherId = teacherId,
                StudentId = model.Id,
                SchoolId = subject.SchoolId,
            };

            await context.Absencess.AddAsync(absence);
            await context.SaveChangesAsync();

            return count + 1 == await context.Absencess.CountAsync();
        }
    }
}
