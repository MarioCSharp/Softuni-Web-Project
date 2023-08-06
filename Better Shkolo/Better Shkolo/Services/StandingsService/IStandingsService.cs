﻿using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Account;
using Better_Shkolo.Models.Standings;

namespace Better_Shkolo.Services.StandingsService
{
    public interface IStandingsService
    {
        Task<StandingsDisplayModel> GetStandings(Student student, string searchTerm, string gradeTerm, int currentPage, int studentsPerPage);
    }
}
