using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Grade;
using Better_Shkolo.Models.StudyPlan;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.StudyPlanService
{
    public class StudyPlanService : IStudyPlanService
    {
        private ApplicationDbContext context;
        public StudyPlanService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create(List<StudyPlanCreateModel> model)
        {
            try
            {
                var schoolId = 0;

                foreach (var item in model)
                {
                    if (schoolId == 0)
                    {
                        var s = await context.Subjects.FindAsync(item.SubjectId);

                        schoolId = s.SchoolId;
                    }

                    var sP = new StudyPlan
                    {
                        Amount = item.Amount,
                        SubjectId = item.SubjectId,
                        SchoolId = schoolId
                    };

                    await context.StudyPlans.AddAsync(sP);
                    await context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int gradeId)
        {
            var ps = await context.StudyPlans
                .Where(x => x.Subject.GradeId == gradeId)
                .ToListAsync();

            context.StudyPlans.RemoveRange(ps);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Edit(List<StudyPlanCreateModel> model)
        {
            foreach (var e in model)
            {
                var p = await context.StudyPlans.FirstOrDefaultAsync(x => x.SubjectId == e.SubjectId);

                p.Amount = e.Amount;
                await context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<List<StudyPlanCreateModel>> GetDetails(int gradeId)
        {
            return await context.StudyPlans
                .Where(x => x.Subject.GradeId == gradeId)
                .Select(x => new StudyPlanCreateModel
                {
                    Amount = x.Amount,
                    SubjectId = x.SubjectId,
                    SubjectName = x.Subject.Name
                }).ToListAsync();
        }

        public async Task<StudyPlanChoseGradeModel> GetGradesInSchool(int schoolId)
        {
            return new StudyPlanChoseGradeModel
            {
                Grades = await context.Grades
                .Where(x => x.SchoolId == schoolId)
                .Select(x => new GradeDisplayModel
                {
                    Id = x.Id,
                    GradeName = x.GradeName
                }).ToListAsync()
            };
        }

        public async Task<StudyPlanViewModel> GetStudyPlans(int schoolId)
        {
            var result = new StudyPlanViewModel()
            {
                SchoolId = schoolId
            };

            var cs = await context.StudyPlans.Where(x => x.SchoolId == schoolId).ToListAsync();

            var res = new List<StudyPlanDisplayModel>();
            var grades = new Dictionary<int, Grade>();

            foreach (var item in cs)
            {
                var s = await context.Subjects.FindAsync(item.SubjectId);

                if (!res.Any(x => x.GradeId == s.GradeId))
                {
                    var g = await context.Grades.FindAsync(s.GradeId);

                    var css = new StudyPlanDisplayModel
                    {
                        GradeId = g.Id,
                        GradeName = g.GradeName
                    };

                    res.Add(css);
                }
            }

            result.StudyPlans = res;

            return result;
        }

        public async Task<List<StudyPlanCreateModel>> GetSubjectsInGrade(int gradeId)
        {
            var subjects = await context.Subjects
                .Where(x => x.GradeId == gradeId)
                .ToListAsync();

            var res = new List<StudyPlanCreateModel>();

            foreach (var subject in subjects)
            {
                res.Add(new StudyPlanCreateModel
                {
                    Amount = 1,
                    SubjectId = subject.Id,
                    SubjectName = subject.Name
                });
            }

            return res;
        }
    }
}
