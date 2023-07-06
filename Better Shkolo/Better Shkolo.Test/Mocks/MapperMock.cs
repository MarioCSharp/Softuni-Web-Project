using AutoMapper;
using Better_Shkolo.Models.Grade;
using Better_Shkolo.Models.Mark;
using Better_Shkolo.Models.Review;
using Better_Shkolo.Models.School;
using Better_Shkolo.Models.Student;
using Better_Shkolo.Models.Subject;

namespace Better_Shkolo.Test.Mocks
{
    public static class MapperMock
    {
        public static IMapper MappingData()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Grade, GradeCreateModel>();
                cfg.CreateMap<School, SchoolCreateModel>();
                cfg.CreateMap<School, SchoolViewModel>();
                cfg.CreateMap<Student, StudentCreateModel>();
                cfg.CreateMap<Subject, SubjectCreateModel>();
                cfg.CreateMap<Grade, GradeDeleteModel>();
                cfg.CreateMap<GradeCreateModel, Grade>();
                cfg.CreateMap<MarkAddModel, Mark>();
                cfg.CreateMap<ReviewAddModel, Review>();
                cfg.CreateMap<Subject, Better_Shkolo.Data.Models.Test>();
                cfg.CreateMap<StudentCreateModel, Student>();
                cfg.CreateMap<SubjectCreateModel, Subject>();
            });

            var mapper = config.CreateMapper();

            return mapper;
        }
    }
}
//this.CreateMap<Grade, GradeCreateModel>();
//this.CreateMap<School, SchoolCreateModel>();
//this.CreateMap<School, SchoolViewModel>();
//this.CreateMap<Student, StudentCreateModel>();
//this.CreateMap<Subject, SubjectCreateModel>();
//this.CreateMap<Grade, GradeDeleteModel>();
//this.CreateMap<GradeCreateModel, Grade>();
//this.CreateMap<MarkAddModel, Mark>();
//this.CreateMap<ReviewAddModel, Review>();
//this.CreateMap<Subject, Test>();
