using AutoMapper;
using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Mark;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.MarkService
{
    public class MarkService : IMarkService
    {
        private ApplicationDbContext context;
        private IMapper mapper;
        public MarkService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<bool> Add(MarkAddModel model, int subjectId, string userId)
        {
            var teacher = await context.Teachers.FirstOrDefaultAsync(x => x.UserId == userId);
            var subject = await context.Subjects.FindAsync(subjectId);
            var student = await context.Students.FindAsync(model.StudentId);

            if (teacher == null || subject == null || student == null || model.Value < 2 || model.Value > 6)
            {
                return false;
            }

            var teacherId = teacher.Id;

            var schoolId = teacher.SchoolId;

            var mark = mapper.Map<Mark>(model);
            mark.AddedOn = DateTime.Now;
            mark.TeacherId = teacherId;
            mark.SchoolId = schoolId;

            await context.Marks.AddAsync(mark);
            await context.SaveChangesAsync();

            return await context.Marks.ContainsAsync(mark);
        }

        public async Task<List<MarkDisplayModel>> GetMarks(string userId)
        {
            var model = new List<MarkDisplayModel>();

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

            var marks = await context.Marks.Where(x => x.StudentId == studentId).ToListAsync();

            foreach (var mark in marks)
            {
                var subjectId = mark.SubjectId;

                if (!model.Any(x => x.SubjectId == subjectId))
                {
                    var subject = await context.Subjects.FindAsync(subjectId);

                    model.Add(new MarkDisplayModel()
                    {
                        SubjectId = subjectId,
                        SubjectName = subject.Name,
                        Marks = new List<MarkViewModel>()
                    });
                }

                var teacher = await context.Teachers.FindAsync(mark.TeacherId);
                var teacherUser = await context.Users.FindAsync(teacher.UserId);

                model.FirstOrDefault(x => x.SubjectId == subjectId).Marks.Add(new MarkViewModel()
                {
                    Id = mark.Id,
                    Value = mark.Value,
                    TeacherId = teacher.Id,
                    TeacherName = teacherUser.FirstName + " " + teacherUser.LastName,
                });
            }

            return model;
        }
    }
}
