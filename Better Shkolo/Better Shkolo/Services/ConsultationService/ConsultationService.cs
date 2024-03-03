using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Consultation;
using Better_Shkolo.Models.Grade;
using Better_Shkolo.Services.AccountService;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.ConsultationService
{
    public class ConsultationService : IConsultationService
    {
        private ApplicationDbContext context;
        private IAccountService accountService;
        public ConsultationService(ApplicationDbContext context
                                   , IAccountService accountService)
        {
            this.context = context;
            this.accountService = accountService;
        }

        public async Task<ConsultationAnalyzeModel> Analyze(int gradeId, string type, int term)
        {
            if (type == "Term" || type == "Year")
            {
                return await TermAnalyze(gradeId, type, term);
            }
            var res = new Dictionary<string, double>();

            var all = await context.Consultations.Where(x => x.GradeId == gradeId && x.Type == type && x.Term == term).ToListAsync();

            var gradeName = string.Empty;

            foreach (var c in all)
            {
                if (string.IsNullOrEmpty(gradeName))
                {
                    var g = await context.Grades.FindAsync(c.GradeId);

                    gradeName = g.GradeName;
                }

                var s = await context.Subjects.FindAsync(c.SubjectId);

                res.Add(s.Name, c.Value);
            }

            var avg = 0.0;

            if (res.Count > 0)
            {
                avg = double.Parse($"{res.Values.Where(x => x >= 2 && x <= 6).Average():F2}");
            }

            var r = new ConsultationAnalyzeModel()
            {
                SubjectByConsultation = res,
                GradeName = gradeName,
                Average = avg
            };

            r.Type = type switch
            {
                "Entry" => "входно ниво",
                "Writting" => "писмено изпитване",
                "Speaking" => "устно изпитване",
                "Test" => "контролно",
                "Project" => "проект",
                "EntActivery" => "активно участие",
                _ => throw new Exception()
            };

            return r;
        }

        public async Task<bool> Create(ConsultationCreateModel model)
        {
            if (await context.Consultations.AnyAsync(x => x.GradeId == model.GradeId && model.Type == x.Type && model.Term == x.Term))
            {
                var toDelete = await context.Consultations.Where(x => x.GradeId == model.GradeId && model.Type == x.Type && model.Term == x.Term).ToListAsync();

                context.Consultations.RemoveRange(toDelete);
                await context.SaveChangesAsync();
            }

            var gradeSubjects = await context.Subjects.Where(x => x.GradeId == model.GradeId).ToListAsync();

            foreach (var subject in gradeSubjects)
            {
                var val = await context.Marks
                    .Where(x => x.SubjectId == subject.Id && x.Type == model.Type && x.Term == model.Term)
                    .ToListAsync();

                var value = 0.0;

                if (val != null && val.Count > 0)
                {
                    value = val.Average(x => x.Value);
                }

                var consultation = new Consultation()
                {
                    Term = model.Term,
                    Type = model.Type,
                    SubjectId = subject.Id,
                    Value = value,
                    UserId = accountService.GetUserId(),
                    GradeId = model.GradeId
                };

                await context.Consultations.AddAsync(consultation);
                await context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> Delete(string userId, int gradeId, string type)
        {
            type = type switch
            {
                "Входно ниво" => "Entry",
                "Писмени изпитване" => "Writting",
                "Устно изпитване" => "Speaking",
                "Контролно" => "Test",
                "Проект" => "Project",
                "Активно участие" => "EntActivery",
                _ => throw new Exception()
            };

            var toDelete = await context.Consultations
                .Where(x => x.UserId == userId && x.GradeId == gradeId && x.Type == type)
                .ToListAsync();

            context.Consultations.RemoveRange(toDelete);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<List<GradeDisplayModel>> GetGrades(string role)
        {
            var grades = new List<GradeDisplayModel>();

            if (role == "director")
            {
                var d = await context.Directors
                    .FirstOrDefaultAsync(x => x.UserId == accountService.GetUserId());

                grades = await context.Grades
                    .Where(x => x.SchoolId == d.SchoolId)
                    .Select(x => new GradeDisplayModel()
                    {
                        Id = x.Id,
                        GradeName = x.GradeName
                    }).ToListAsync();
            }
            else if (role == "teacher")
            {
                var t = await context.Teachers.
                    FirstOrDefaultAsync(x => x.UserId == accountService.GetUserId());

                grades = await context.Subjects
                    .Where(x => x.TeacherId == t.Id)
                    .Select(x => new GradeDisplayModel()
                    {
                        Id = x.Grade.Id,
                        GradeName = x.Grade.GradeName
                    }).ToListAsync();

                var res = new List<GradeDisplayModel>();

                foreach (var g in grades)
                {
                    if (!res.Any(x => x.Id == g.Id))
                    {
                        res.Add(g);
                    }
                }

                grades = res;
            }

            return grades;
        }

        public async Task<List<ConsultationMineModel>> GetUserConsultations()
        {
            var cs = await context.Consultations
                .Where(x => x.UserId == accountService.GetUserId())
                .ToListAsync();

            var res = new List<ConsultationMineModel>();

            foreach (var item in cs)
            {
                if (!res.Any(x => x.UserId == item.UserId
                && x.GradeId == item.GradeId && x.Type == item.Type))
                {
                    var grade = await context.Grades.FindAsync(item.GradeId);

                    var css = new ConsultationMineModel
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        GradeId = item.GradeId,
                        Type = item.Type,
                        GradeName = grade.GradeName,
                        Term = item.Term,
                    };

                    res.Add(css);
                }
            }

            foreach (var item in res)
            {
                item.Type = item.Type switch
                {
                    "Entry" => "Входно ниво",
                    "Writting" => "Писмено изпитване",
                    "Speaking" => "Устно изпитване",
                    "Test" => "Контролно",
                    "Project" => "Проект",
                    "EntActivery" => "Активно участие",
                    "Term" => "Срочни оценки",
                    "Year" => "Годишни оценки",
                    _ => throw new Exception()
                };
            }

            return res;
        }

        public async Task<ConsultationAnalyzeModel> TermAnalyze(int gradeId, string type, int term)
        {
            if (type == "Term")
            {
                var marks = await context.TermMarks
                    .Where(x => x.Subject.GradeId == gradeId && x.Term == term)
                    .ToListAsync();

                var ret = new Dictionary<string, double>();

                var gradeName = string.Empty;

                foreach (var mark in marks)
                {
                    var g = await context.Subjects.FindAsync(mark.SubjectId);

                    if (string.IsNullOrEmpty(gradeName))
                    {
                        var grade = await context.Grades.FindAsync(g.GradeId);
                        gradeName = grade.GradeName;
                    }

                    ret.Add(g.Name, mark.Value);
                }
                var avg = 0.0;

                if (ret.Count > 0)
                {
                    avg = double.Parse($"{ret.Values.Average():F2}");
                }
                return new ConsultationAnalyzeModel()
                {
                    GradeName = gradeName,
                    Type = $"{(term == 1 ? "първи" : "втори")} срок",
                    Average = avg,
                    SubjectByConsultation = ret
                };
            }
            else
            {
                var marks = await context.YearMarks
                    .Where(x => x.Subject.GradeId == gradeId)
                    .ToListAsync();

                var ret = new Dictionary<string, double>();

                var gradeName = string.Empty;

                foreach (var mark in marks)
                {
                    var g = await context.Subjects.FindAsync(mark.SubjectId);

                    if (string.IsNullOrEmpty(gradeName))
                    {
                        gradeName = g.Name;
                    }

                    ret.Add(g.Name, mark.Value);
                }

                var avg = 0.0;

                if (ret.Count > 0)
                {
                    avg = double.Parse($"{ret.Values.Average():F2}");
                }

                return new ConsultationAnalyzeModel()
                {
                    GradeName = gradeName,
                    Type = $"годишни оценки",
                    Average = avg,
                    SubjectByConsultation = ret
                };
            }
        }
    }
}
