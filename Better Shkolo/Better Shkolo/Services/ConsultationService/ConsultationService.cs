using Better_Shkolo.Data;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Consultation;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.ConsultationService
{
    public class ConsultationService : IConsultationService
    {
        private ApplicationDbContext context;
        public ConsultationService(ApplicationDbContext context)
        {
            this.context = context;
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

            var r = new ConsultationAnalyzeModel()
            { 
                SubjectByConsultation = res,
                GradeName = gradeName, 
                Average = double.Parse($"{res.Values.Average():F2}")
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
                    GradeId = model.GradeId
                };

                await context.Consultations.AddAsync(consultation);
                await context.SaveChangesAsync();
            }

            return true;
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

                return new ConsultationAnalyzeModel()
                {
                    GradeName = gradeName,
                    Type = $"{(term == 1 ? "първи" : "втори")} срок",
                    Average = double.Parse($"{ret.Values.Average():F2}"),
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

                return new ConsultationAnalyzeModel()
                {
                    GradeName = gradeName,
                    Type = $"годишни оценки",
                    Average = double.Parse($"{ret.Values.Average():F2}"),
                    SubjectByConsultation = ret
                };
            }
        }
    }
}
