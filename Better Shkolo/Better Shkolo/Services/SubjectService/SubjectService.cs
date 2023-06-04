using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Subject;
using Better_Shkolo.Services.AccountService;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.SubjectService
{
    public class SubjectService : ISubjectService
    {
        private ApplicationDbContext context;
        private IAccountService accountService;
        public SubjectService(ApplicationDbContext context,
                              IAccountService accountService)
        {
            this.context = context;
            this.accountService = accountService;
        }
        public async Task<bool> Create(SubjectCreateModel model)
        {
            var count = await context.Subjects.CountAsync();

            var subject = new Subject()
            {
                Name = model.Name,
                TeacherId = model.TeacherId,
                SchoolId = model.SchoolId,
                GradeId = model.GradeId
            };

            await context.Subjects.AddAsync(subject);
            await context.SaveChangesAsync();

            return count + 1 == await context.Subjects.CountAsync();
        }

        public async Task<bool> DeleteSubject(int id)
        {
            var subject = await context.Subjects.FindAsync(id);

            if (subject == null)
            {
                return false;
            }

            var count = await context.Subjects.CountAsync();

            context.Absencess.RemoveRange(context.Absencess.Where(x => x.SubjectId == subject.Id).ToArray());
            context.Marks.RemoveRange(context.Marks.Where(x => x.SubjectId == subject.Id).ToArray());
            context.Reviews.RemoveRange(context.Reviews.Where(x => x.SubjectId == subject.Id).ToArray());
            context.Tests.RemoveRange(context.Tests.Where(x => x.SubjectId == subject.Id).ToArray());

            context.Subjects.Remove(subject);
            await context.SaveChangesAsync();

            return count - 1 == await context.Subjects.CountAsync();
        }

        public async Task<bool> Edit(SubjectCreateModel model, int id)
        {
            var subject = await GetSubject(id);

            if (subject is null)
            {
                return false;
            }

            var marks = await context.Marks.Where(x => x.TeacherId == subject.TeacherId).ToListAsync();
            var reviews = await context.Reviews.Where(x => x.TeacherId == subject.TeacherId).ToListAsync();
            var absenceses = await context.Absencess.Where(x => x.TeacherId == subject.TeacherId).ToListAsync();
            var tests = await context.Tests.Where(x => x.TeacherId == subject.TeacherId).ToListAsync();

            foreach (var mark in marks)
            {
                mark.TeacherId = model.TeacherId;
                await context.SaveChangesAsync();
            }

            foreach (var review in reviews)
            {
                review.TeacherId = model.TeacherId;
                await context.SaveChangesAsync();
            }

            foreach (var absences in absenceses)
            {
                absences.TeacherId = model.TeacherId;
                await context.SaveChangesAsync();
            }

            foreach (var test in tests)
            {
                test.TeacherId = model.TeacherId;
                await context.SaveChangesAsync();
            }

            subject.Name = model.Name;
            subject.SchoolId = model.SchoolId;
            subject.TeacherId = model.TeacherId;
            subject.GradeId = model.GradeId;

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<Subject> GetSubject(int id)
        {
            return await context.Subjects.FindAsync(id);
        }

        public async Task<List<SubjectDisplayModel>> GetSubjectsBySchoolId(int Id)
        {
            return await context.Subjects.Where(x => x.SchoolId == Id).Select(x => new SubjectDisplayModel
            {
                Id = x.Id,
                TeacherId = x.TeacherId,
                Name = x.Name,
            }).ToListAsync();
        }

        public async Task<List<SubjectDisplayModel>> GetSubjectsByTeacherId(int id)
        {
            var result = await context.Subjects.Where(x => x.TeacherId == id)
                    .Select(x => new SubjectDisplayModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        TeacherId = x.TeacherId
                    }).ToListAsync();

            return result;
        }

        public async Task<List<SubjectDisplayModel>> GetSubjectsByUser()
        {
            var userId = accountService.GetUserId();

            var teacher = await context.Teachers.FirstOrDefaultAsync(x => x.UserId == userId);

            return await context.Subjects.Where(x => x.TeacherId == teacher.Id)
                .Select(x => new SubjectDisplayModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    TeacherId = teacher.Id
                }).ToListAsync() ;
        }
    }
}
