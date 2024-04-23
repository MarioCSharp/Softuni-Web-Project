using Better_Shkolo.Models.Team;

namespace Better_Shkolo.Services.TeamService
{
    public interface ITeamService
    {
        Task<string> GetRoomId(int teamId);
        Task<TeamAddModel> GetModel();
        Task<bool> AddAsync(TeamAddModel model);
        Task<List<TeamDisplayModel>> GetDirectorIndexModel();
        Task<List<TeamDisplayModel>> GetStudentIndexModel();
        Task<List<TeamDisplayModel>> GetTeacherIndexModel();
        Task<TeamDetailsModel> GetTeamDetails(int teamId);
    }
}
