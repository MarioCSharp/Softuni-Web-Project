using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Mark;
using Better_Shkolo.Services.AccountService;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.MarkService
{
    public class MarkService : IMarkService
    {
        private ApplicationDbContext context;
        private IAccountService accountService;
        public MarkService(ApplicationDbContext context,
                           IAccountService accountService)
        {
            this.context = context;
            this.accountService = accountService;
        }
        public async Task<bool> Add(MarkAddModel model, int subjectId)
        {
            var teacher = await context.Teachers.FirstOrDefaultAsync(x => x.UserId == accountService.GetUserId());

            if (teacher == null)
            {
                return false;
            }

            var count = await context.Marks.CountAsync();

            var teacherId = teacher.Id;

            var schoolId = teacher.SchoolId;

            var mark = new Mark()
            {
                Value = model.Value,
                AddedOn = DateTime.Now,
                SubjectId = subjectId,
                TeacherId = teacherId,
                StudentId = model.StudentId,
                SchoolId = schoolId
            };

            await context.Marks.AddAsync(mark);
            await context.SaveChangesAsync();

            return count + 1 == await context.Marks.CountAsync();
        }
    }
}
