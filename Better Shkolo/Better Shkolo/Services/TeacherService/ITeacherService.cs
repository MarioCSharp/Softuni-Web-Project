﻿using Better_Shkolo.Models.Teacher;

namespace Better_Shkolo.Services.TeacherService
{
    public interface ITeacherService
    {
        List<TeacherDisplayModel> GetAllTeacherInSchool(int schoolId, string userId);
        bool Create(TeacherCreateModel model);
        bool DeleteTeacher(int id);
    }
}
