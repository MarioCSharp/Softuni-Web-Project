using AutoMapper;
using BetterShkolo.Data;
using BetterShkolo.Data.Models;
using BetterShkolo.Models.Subject;
using BetterShkolo.Services.AccountService;
using Microsoft.EntityFrameworkCore;

namespace BetterShkolo.Services.SubjectService
{
    public class SubjectService : ISubjectService
    {
        private ApplicationDbContext context;
        private IAccountService accountService;
        private IMapper mapper;
        public SubjectService(ApplicationDbContext context,
                              IAccountService accountService,
                              IMapper mapper)
        {
            this.context = context;
            this.accountService = accountService;
            this.mapper = mapper;
        }
        public async Task<bool> Create(SubjectCreateModel model)
        {
            if (!await context.Grades.AnyAsync(x => x.Id == model.GradeId)
                || !await context.Schools.AnyAsync(x => x.Id == model.SchoolId)
                || !await context.Teachers.AnyAsync(x => x.Id == model.TeacherId))
            {
                return false;
            }

            var subject = mapper.Map<Subject>(model);

            await context.Subjects.AddAsync(subject);
            await context.SaveChangesAsync();

            return await context.Subjects.ContainsAsync(subject);
        }

        public async Task<bool> DeleteSubject(int id)
        {
            var subject = await context.Subjects.FindAsync(id);

            if (subject == null)
            {
                return false;
            }

            context.Absencess.RemoveRange(await context.Absencess.Where(x => x.SubjectId == subject.Id).ToArrayAsync());
            context.Marks.RemoveRange(await context.Marks.Where(x => x.SubjectId == subject.Id).ToArrayAsync());
            context.Reviews.RemoveRange(await context.Reviews.Where(x => x.SubjectId == subject.Id).ToArrayAsync());
            context.Tests.RemoveRange(await context.Tests.Where(x => x.SubjectId == subject.Id).ToArrayAsync());

            context.Subjects.Remove(subject);
            await context.SaveChangesAsync();

            return !await context.Subjects.ContainsAsync(subject);
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
            subject.Type = model.Type;

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<Subject> GetSubject(int id)
        {
            return await context.Subjects.FindAsync(id);
        }

        public async Task<List<SubjectDisplayModel>> GetSubjectsByGrade(int gradeId)
        {
            var userId = accountService.GetUserId();

            var teacher = await context.Teachers.FirstOrDefaultAsync(x => x.UserId == userId);

            if (teacher is null)
            {
                return null;
            }

            var res = await context.Subjects.Where(x => x.GradeId == gradeId && x.TeacherId == teacher.Id)
                .Select(x => new SubjectDisplayModel
                {
                    Id = x.Id,
                    TeacherId = x.TeacherId,
                    Name = x.Name,
                }).ToListAsync();

            return res;
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

            if (teacher is null)
            {
                return null;
            }

            return await context.Subjects.Where(x => x.TeacherId == teacher.Id)
                .Select(x => new SubjectDisplayModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    TeacherId = teacher.Id
                }).ToListAsync();
        }
    }
}
