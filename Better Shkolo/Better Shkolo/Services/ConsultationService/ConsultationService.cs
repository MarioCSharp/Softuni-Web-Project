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

            var r = new ConsultationAnalyzeModel() { SubjectByConsultation = res,  GradeName = gradeName};

            r.Type = type switch
            {
                "Entry" => "Входно ниво",
                "Writting" => "Писмено изпитване",
                "Speaking" => "Устно изпитване",
                "Test" => "Контролно",
                "Project" => "Проект",
                "EntActivery" => "Активно участие",
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
    }
}
