using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Absence;
using Better_Shkolo.Models.Mark;
using Better_Shkolo.Services.AccountService;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.AbsenceService
{
    public class AbsencesService : IAbsenceService
    {
        private ApplicationDbContext context;
        private IAccountService accountService;
        public AbsencesService(ApplicationDbContext context, IAccountService accountService)
        {
            this.context = context;
            this.accountService = accountService;
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

        public async Task<List<AbsencesesDisplayModel>> GetAbsenceses()
        {
            var userId = accountService.GetUserId();

            var model = new List<AbsencesesDisplayModel>();

            var student = await context.Students.FirstOrDefaultAsync(x => x.UserId == userId);

            var studentId = student.Id;

            var absenceses = await context.Absencess.Where(x => x.StudentId == studentId).ToListAsync();

            foreach (var absences in absenceses)
            {
                var subjectId = absences.SubjectId;

                if (!model.Any(x => x.SubjectId == subjectId))
                {
                    var subject = await context.Subjects.FindAsync(subjectId);

                    model.Add(new AbsencesesDisplayModel()
                    {
                        SubjectId = subjectId,
                        SubjectName = subject.Name,
                        Absenceses = new List<AbsencesViewModel>()
                    });


                }

                var teacher = await context.Teachers.FindAsync(absences.TeacherId);
                var teacherUser = await context.Users.FindAsync(teacher.UserId);

                model.FirstOrDefault(x => x.SubjectId == subjectId).Absenceses.Add(new AbsencesViewModel()
                {
                    Id = absences.Id,
                    DateTime = absences.AddedOn,
                    TeacherId = teacher.Id,
                    TeacherName = teacherUser.FirstName + " " + teacherUser.LastName,
                });
            }

            return model;
        }
    }
}
