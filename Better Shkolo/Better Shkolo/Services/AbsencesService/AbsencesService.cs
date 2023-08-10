using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Absence;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.AbsenceService
{
    public class AbsencesService : IAbsencesService
    {
        private ApplicationDbContext context;
        public AbsencesService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> Add(AbsencesAddModel model)
        {
            var subject = await context.Subjects.FindAsync(model.SubjectId);
            var student = await context.Students.FindAsync(model.Id);
            var school = await context.Schools.FindAsync(model.SchoolId);

            if (subject is null || student is null || school is null)
            {
                return false;
            }

            var teacherId = subject.TeacherId;

            var absence = new Absences()
            {
                AddedOn = DateTime.Now,
                SubjectId = subject.Id,
                TeacherId = teacherId,
                StudentId = student.Id,
                SchoolId = subject.SchoolId,
            };

            await context.Absencess.AddAsync(absence);
            await context.SaveChangesAsync();

            return await context.Absencess.ContainsAsync(absence);
        }

        public async void Excuse(Absences absences)
        {
            absences.ExcusedOn = DateTime.Now;
            context.SaveChanges();
        }

        public async Task<Absences> GetAbsences(int id)
        {
            return await context.Absencess.FindAsync(id);
        }

        public async Task<List<AbsencesesDisplayModel>> GetAbsenceses(string userId)
        {
            var model = new List<AbsencesesDisplayModel>();

            var student = await context.Students.FirstOrDefaultAsync(x => x.UserId == userId);

            var studentId = 0;

            if (student == null)
            {
                var parent = await context.Parents.FirstOrDefaultAsync(x => x.UserId == userId);

                studentId = parent.StudentId;
            }
            else
            {
                studentId = student.Id;
            }

            if (studentId == 0)
            {
                return null;
            }

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

        public async Task<List<AbsencesesShowModel>> GetAbsencesesBySubjectId(string userId, int subjectId)
        {
            var student = await context.Students.FirstOrDefaultAsync(x => x.UserId == userId);

            var studentId = 0;

            if (student is null)
            {
                var parent = await context.Parents.FirstOrDefaultAsync(x => x.UserId == userId);

                if (parent is null)
                {
                    return null;
                }

                studentId = parent.StudentId;
            }
            else
            {
                studentId = student.Id;
            }

            return await context.Absencess
                .Where(x => x.StudentId == studentId && x.SubjectId == subjectId)
                .Select(x => new AbsencesesShowModel()
                {
                    Id = x.Id,
                    SubjectId = x.SubjectId,
                    AddedOn = x.AddedOn,
                    ExcusedOn = x.ExcusedOn,
                    SubjectName = x.Subject.Name
                })
                .ToListAsync();
        }

        public async Task<List<AbsencesesShowModel>> GetAllStudentAbsenceses(int studentId)
        {
            return await context.Absencess
                .Where(x => x.StudentId == studentId)
                .Select(x => new AbsencesesShowModel
                {
                    Id = x.Id,
                    AddedOn = x.AddedOn,
                    ExcusedOn = x.ExcusedOn,
                    SubjectName = x.Subject.Name
                }).ToListAsync();
        }
    }
}
