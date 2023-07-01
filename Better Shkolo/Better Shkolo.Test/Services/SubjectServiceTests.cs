using Better_Shkolo.Models.Subject;
using Better_Shkolo.Services.SubjectService;
using Better_Shkolo.Test.Mocks;

namespace Better_Shkolo.Test.Services
{
    public class SubjectServiceTests
    {
        [Fact]
        public async void CreateSubjectWorksCorrectly()
        {
            using var data = DatabaseMock.Instance;

            await data.Schools.AddAsync(new School()
            {
                Id = 23,
                Name = "asdas",
                DirectorId = "test",
                City = "asdtest"
            });
            await data.SaveChangesAsync();

            await data.Teachers.AddAsync(new Teacher()
            {
                Id = 13,
                SchoolId = 2,
                UserId = "test"
            });
            await data.SaveChangesAsync();

            await data.Grades.AddAsync(new Grade()
            {
                Id = 4,
                SchoolId = 2,
                GradeName = "test",
                GradeSpecialty = "test",
                TeacherId = 13,
            });
            await data.SaveChangesAsync();

            var service = new SubjectService(data, null);

            var result = await service.Create(new SubjectCreateModel()
            {
                Name = "test",
                TeacherId = 13,
                SchoolId = 23,
                GradeId = 4
            });

            Assert.True(result);
        }

        [Fact]
        public async void CreateSubjectFailsBecauseOfNullSchool()
        {
            using var data = DatabaseMock.Instance;

            await data.Teachers.AddAsync(new Teacher()
            {
                Id = 13,
                SchoolId = 2,
                UserId = "test"
            });
            await data.SaveChangesAsync();

            await data.Grades.AddAsync(new Grade()
            {
                Id = 4,
                SchoolId = 2,
                GradeName = "test",
                GradeSpecialty = "test",
                TeacherId = 13,
            });
            await data.SaveChangesAsync();

            var service = new SubjectService(data, null);

            var result = await service.Create(new SubjectCreateModel()
            {
                Name = "test",
                TeacherId = 13,
                SchoolId = 23,
                GradeId = 4
            });

            Assert.False(result);
        }

        [Fact]
        public async void CreateSubjectFailsBecauseOfNullTeacher()
        {
            using var data = DatabaseMock.Instance;

            await data.Schools.AddAsync(new School()
            {
                Id = 23,
                Name = "asdas",
                DirectorId = "test",
                City = "asdtest"
            });
            await data.SaveChangesAsync();

            await data.Grades.AddAsync(new Grade()
            {
                Id = 4,
                SchoolId = 2,
                GradeName = "test",
                GradeSpecialty = "test",
                TeacherId = 13,
            });
            await data.SaveChangesAsync();

            var service = new SubjectService(data, null);

            var result = await service.Create(new SubjectCreateModel()
            {
                Name = "test",
                TeacherId = 13,
                SchoolId = 23,
                GradeId = 4
            });

            Assert.False(result);
        }

        [Fact]
        public async void CreateSubjectFailsBecauseOfNullGrade()
        {
            using var data = DatabaseMock.Instance;

            await data.Schools.AddAsync(new School()
            {
                Id = 23,
                Name = "asdas",
                DirectorId = "test",
                City = "asdtest"
            });
            await data.SaveChangesAsync();

            await data.Teachers.AddAsync(new Teacher()
            {
                Id = 13,
                SchoolId = 2,
                UserId = "test"
            });
            await data.SaveChangesAsync();

            var service = new SubjectService(data, null);

            var result = await service.Create(new SubjectCreateModel()
            {
                Name = "test",
                TeacherId = 13,
                SchoolId = 23,
                GradeId = 4
            });

            Assert.False(result);
        }

        [Fact]
        public async void DeleteSubjectWorksCorrectly()
        {
            using var data = DatabaseMock.Instance;

            await data.Schools.AddAsync(new School()
            {
                Id = 23,
                Name = "asdas",
                DirectorId = "test",
                City = "asdtest"
            });
            await data.SaveChangesAsync();

            await data.Teachers.AddAsync(new Teacher()
            {
                Id = 13,
                SchoolId = 2,
                UserId = "test"
            });
            await data.SaveChangesAsync();

            await data.Grades.AddAsync(new Grade()
            {
                Id = 4,
                SchoolId = 2,
                GradeName = "test",
                GradeSpecialty = "test",
                TeacherId = 13,
            });
            await data.SaveChangesAsync();

            var service = new SubjectService(data, null);

            await service.Create(new SubjectCreateModel()
            {
                Name = "test",
                TeacherId = 13,
                SchoolId = 23,
                GradeId = 4
            });

            var result = await service.DeleteSubject(1);

            Assert.True(result);
        }

        [Fact]
        public async void DeleteSubjectsFailsBecauseOfNullSubejct()
        {
            using var data = DatabaseMock.Instance;

            await data.Schools.AddAsync(new School()
            {
                Id = 23,
                Name = "asdas",
                DirectorId = "test",
                City = "asdtest"
            });
            await data.SaveChangesAsync();

            await data.Teachers.AddAsync(new Teacher()
            {
                Id = 13,
                SchoolId = 2,
                UserId = "test"
            });
            await data.SaveChangesAsync();

            await data.Grades.AddAsync(new Grade()
            {
                Id = 4,
                SchoolId = 2,
                GradeName = "test",
                GradeSpecialty = "test",
                TeacherId = 13,
            });
            await data.SaveChangesAsync();

            var service = new SubjectService(data, null);

            await service.Create(new SubjectCreateModel()
            {
                Name = "test",
                TeacherId = 13,
                SchoolId = 23,
                GradeId = 4
            });

            var result = await service.DeleteSubject(2);

            Assert.False(result);
        }

        [Fact]
        public async void GetSubjectWorksCorrectly()
        {
            using var data = DatabaseMock.Instance;

            var subject = new Subject()
            {
                Id = 23,
                Name = "asdas",
                TeacherId = 4,
                SchoolId = 23,
                GradeId = 33
            };

            await data.Subjects.AddAsync(subject);
            await data.SaveChangesAsync();

            var service = new SubjectService(data, null);
            
            var result = await service.GetSubject(23);

            Assert.Equal(subject, result);
        }
    }
}
