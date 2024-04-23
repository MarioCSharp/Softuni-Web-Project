using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Team;
using BetterShkolo.Data;
using BetterShkolo.Services.AccountService;
using BetterShkolo.Services.GradeService;
using BetterShkolo.Services.SchoolService;
using BetterShkolo.Services.TeacherService;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Services.TeamService
{
    public class TeamService : ITeamService
    {
        private ApplicationDbContext context;
        private ISchoolService schoolService;
        private ITeacherService teacherService;
        private IGradeService gradeService;
        private IAccountService accountService;
        public TeamService(ApplicationDbContext context,
                           ISchoolService schoolService,
                           ITeacherService teacherService,
                           IGradeService gradeService,
                           IAccountService accountService)
        {
            this.context = context;
            this.schoolService = schoolService;
            this.teacherService = teacherService;
            this.gradeService = gradeService;
            this.accountService = accountService;
        }

        public async Task<bool> AddAsync(TeamAddModel model)
        {
            var team = new Team()
            {
                GradeId = model.GradeId,
                TeacherId = model.TeacherId,
                Name = model.Name,
                RoomId = Guid.NewGuid().ToString()
            };

            await context.Teams.AddAsync(team);
            await context.SaveChangesAsync();

            return await context.Teams.ContainsAsync(team);
        }

        public async Task<List<TeamDisplayModel>> GetDirectorIndexModel()
        {
            var sId = await schoolService.GetSchoolIdByUser();

            return await context.Teams
                .Where(x => x.Grade.SchoolId == sId)
                .Select(x => new TeamDisplayModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    GradeName = x.Grade.GradeName,
                    TeacherName = x.Teacher.User.FirstName + " " + x.Teacher.User.LastName
                }).ToListAsync();
        }

        public async Task<TeamAddModel> GetModel()
        {
            var sId = await schoolService.GetSchoolIdByUser();

            return new TeamAddModel
            {
                Teachers = await teacherService.GetAllTeacherInSchool(sId),
                Grades = await gradeService.GetGradesBySchoolId(sId)
            };
        }

        public async Task<string> GetRoomId(int teamId)
        {
            var team = await context.Teams.FindAsync(teamId);

            if (team is null) return null;

            return team.RoomId;
        }

        public async Task<List<TeamDisplayModel>> GetStudentIndexModel()
        {
            var uId = accountService.GetUserId();

            var student = await context.Students.FirstOrDefaultAsync(x => x.UserId == uId);

            if (student is null)
            {
                return new List<TeamDisplayModel> { };
            }

            return await context.Teams
                .Where(x => x.GradeId == student.GradeId)
                .Select(x => new TeamDisplayModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    GradeName = x.Grade.GradeName,
                    TeacherName = x.Teacher.User.FirstName + " " + x.Teacher.User.LastName
                }).ToListAsync();
        }

        public async Task<List<TeamDisplayModel>> GetTeacherIndexModel()
        {
            var uId = accountService.GetUserId();

            var teacher = await context.Teachers.FirstOrDefaultAsync(x => x.UserId == uId);

            if (teacher is null)
            {
                return new List<TeamDisplayModel> { };
            }

            return await context.Teams
                .Where(x => x.TeacherId == teacher.Id)
                .Select(x => new TeamDisplayModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    GradeName = x.Grade.GradeName,
                    TeacherName = x.Teacher.User.FirstName + " " + x.Teacher.User.LastName
                }).ToListAsync();
        }

        public async Task<TeamDetailsModel> GetTeamDetails(int teamId)
        {
            var team = await context.Teams.FindAsync(teamId);

            if (team is null)
            {
                return null;
            }

            var grade = await context.Grades.FindAsync(team.GradeId);
            var teacher = await context.Teachers.FindAsync(team.TeacherId);
            var userT = await context.Users.FindAsync(teacher.UserId);

            return new TeamDetailsModel
            {
                Id = team.Id,
                Name = team.Name,
                GradeName= grade.GradeName,
                TeacherName = userT.FirstName + " " + userT.LastName,
                RoomId = team.RoomId
            };
        }
    }
}
