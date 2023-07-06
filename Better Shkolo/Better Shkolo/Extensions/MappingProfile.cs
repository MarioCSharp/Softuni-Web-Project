using AutoMapper;
using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Grade;
using Better_Shkolo.Models.Mark;
using Better_Shkolo.Models.Review;
using Better_Shkolo.Models.School;
using Better_Shkolo.Models.Student;
using Better_Shkolo.Models.Subject;

namespace Better_Shkolo.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            this.CreateMap<Grade, GradeCreateModel>();
            this.CreateMap<School, SchoolCreateModel>();
            this.CreateMap<School, SchoolViewModel>();
            this.CreateMap<Student, StudentCreateModel>();
            this.CreateMap<Subject, SubjectCreateModel>();
            this.CreateMap<Grade, GradeDeleteModel>();
            this.CreateMap<GradeCreateModel, Grade>();
            this.CreateMap<MarkAddModel, Mark>();
            this.CreateMap<ReviewAddModel, Review>();
            this.CreateMap<Subject, Test>();
            this.CreateMap<StudentCreateModel, Student>();
            this.CreateMap<SubjectCreateModel, Subject>();
        }
    }
}
