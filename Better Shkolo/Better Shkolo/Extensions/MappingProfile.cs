using AutoMapper;
using BetterShkolo.Data.Models;
using BetterShkolo.Models.Grade;
using BetterShkolo.Models.Mark;
using BetterShkolo.Models.Review;
using BetterShkolo.Models.School;
using BetterShkolo.Models.Student;
using BetterShkolo.Models.Subject;

namespace BetterShkolo.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Grade, GradeCreateModel>();
            CreateMap<School, SchoolCreateModel>();
            CreateMap<School, SchoolViewModel>();
            CreateMap<Student, StudentCreateModel>();
            CreateMap<Subject, SubjectCreateModel>();
            CreateMap<Grade, GradeDeleteModel>();
            CreateMap<GradeCreateModel, Grade>();
            CreateMap<MarkAddModel, Mark>();
            CreateMap<ReviewAddModel, Review>();
            CreateMap<Subject, Test>();
            CreateMap<StudentCreateModel, Student>();
            CreateMap<SubjectCreateModel, Subject>();
        }
    }
}
