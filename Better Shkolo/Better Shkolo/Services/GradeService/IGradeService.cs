﻿using BetterShkolo.Data.Models;
using BetterShkolo.Models.Grade;
using BetterShkolo.Models.Student;

namespace BetterShkolo.Services.GradeService
{
    public interface IGradeService
    {
        Task<bool> Create(GradeCreateModel model);
        Task<bool> DeleteGrade(int id);
        Task<Grade> GetGrade(int id);
        Task<List<GradeDisplayModel>> GetGradesBySchoolId(int schoolId);
        Task<Grade> GetGradeByTeacherId(int teacherId);
        Task<List<StudentDisplayModel>> GetStudentsInGrade(string userId);
        Task<GradeStatisticsModel> GetGradeStatistics(Grade grade);
        Task<List<StudentDisplayModel>> GetStudentsInGrade(int id);
        Task<List<GradeDisplayModel>> GetAllGradesAsync();
        Task<List<GradeDisplayModel>> GetSchoolGradesAsync();
        Task<List<GradeDisplayModel>> GetTeacherGradesAsync();
        Task<int> GetUserGradeId();
    }
}
