using Better_Shkolo.Models.Grade;
using Better_Shkolo.Services.GradeService;
using Better_Shkolo.Test.Mocks;

namespace Better_Shkolo.Test.Services
{
    public class GradeServiceTests
    {
        [Fact]
        public async void CreateGradeWithNullGradeNameShouldReturnFalse()
        {
            using var data = DatabaseMock.Instance;

            var service = new GradeService(data, null, MapperMock.MappingData());

            var result = await service.Create(new GradeCreateModel()
            {
                GradeName = null,
                GradeSpecialty = "asd",
                SchoolId = 1,
                TeacherId = 1,
            });

            Assert.False(result);
        }

        [Fact]
        public async void CreateGradeWithNullGradeSpecialtyShouldReturnFalse()
        {
            using var data = DatabaseMock.Instance;

            var service = new GradeService(data, null, MapperMock.MappingData());

            var result = await service.Create(new GradeCreateModel()
            {
                GradeName = "asd",
                GradeSpecialty = null,
                SchoolId = 1,
                TeacherId = 1,
            });

            Assert.False(result);
        }

        [Fact]
        public async void CreateGradeWithInvalidSchoolShouldReturnFalse()
        {
            using var data = DatabaseMock.Instance;

            await data.Teachers.AddAsync(new Teacher()
            {
                Id = 2,
                SchoolId = 3,
                UserId = "adasfas"
            });

            await data.SaveChangesAsync();

            var service = new GradeService(data, null, MapperMock.MappingData());

            var result = await service.Create(new GradeCreateModel()
            {
                GradeName = "asd",
                GradeSpecialty = "asd",
                SchoolId = 1,
                TeacherId = 2,
            });

            Assert.False(result);
        }

        [Fact]
        public async void CreateGradeWithInvalidTeacherShouldReturnFalse()
        {
            using var data = DatabaseMock.Instance;

            await data.Schools.AddAsync(new School()
            {
                Id = 2,
                Name = "Name",
                City = "asdasd",
                DirectorId = "asd"
            });

            await data.SaveChangesAsync();

            var service = new GradeService(data, null, MapperMock.MappingData());

            var result = await service.Create(new GradeCreateModel()
            {
                GradeName = "asd",
                GradeSpecialty = "asd",
                SchoolId = 23,
                TeacherId = 2,
            });

            Assert.False(result);
        }

        [Fact]
        public async void CreateGradeShouldReturnTrue()
        {
            using var data = DatabaseMock.Instance;

            await data.Schools.AddAsync(new School()
            {
                Id = 2,
                Name = "Name",
                City = "asdasd",
                DirectorId = "asd"
            });

            await data.Teachers.AddAsync(new Teacher()
            {
                Id = 23,
                SchoolId = 2,
                UserId = "asdasda"
            });

            await data.SaveChangesAsync();

            var service = new GradeService(data, null, MapperMock.MappingData());

            var result = await service.Create(new GradeCreateModel()
            {
                GradeName = "asd",
                GradeSpecialty = "asd",
                SchoolId = 2,
                TeacherId = 23,
            });

            Assert.True(result);
        }

        [Fact]
        public async void DeleteGradeWithInvalidGradeShouldReturnFalse()
        {
            using var data = DatabaseMock.Instance;

            var service = new GradeService(data, null, MapperMock.MappingData());

            var result = await service.DeleteGrade(31);

            Assert.False(result);
        }

        [Fact]
        public async void GetGradeWithInvalidIdShouldReturnNull()
        {
            using var data = DatabaseMock.Instance;

            var service = new GradeService(data, null, MapperMock.MappingData());

            var result = await service.GetGrade(31);

            Assert.Null(result);
        }

        [Fact]
        public async void GetGradeShouldReturnCorrectGrade()
        {
            using var data = DatabaseMock.Instance;

            await data.Grades.AddAsync(new Grade()
            {
                Id = 233,
                GradeName = "asd",
                GradeSpecialty = "asd",
                TeacherId = 23,
                SchoolId = 2
            });
            await data.SaveChangesAsync();

            var grade = new Grade()
            {
                Id = 23,
                GradeName = "the23grade",
                GradeSpecialty = "the23speciality",
                TeacherId = 23,
                SchoolId = 2
            };

            await data.Grades.AddAsync(grade);
            await data.SaveChangesAsync();

            var service = new GradeService(data, null, MapperMock.MappingData());

            var result = await service.GetGrade(23);

            Assert.Equal(grade, result);
        }

        [Fact]
        public async void GetGradeByTeacherIdShouldReturnNull()
        {
            using var data = DatabaseMock.Instance;

            await data.Grades.AddAsync(new Grade()
            {
                Id = 233,
                GradeName = "asd",
                GradeSpecialty = "asd",
                TeacherId = 23,
                SchoolId = 2
            });
            await data.SaveChangesAsync();

            var grade = new Grade()
            {
                Id = 23,
                GradeName = "the23grade",
                GradeSpecialty = "the23speciality",
                TeacherId = 23,
                SchoolId = 2
            };

            var service = new GradeService(data, null, MapperMock.MappingData());

            var result = await service.GetGradeByTeacherId(31);

            Assert.Null(result);
        }

        [Fact]
        public async void GetGradesBySchoolIdShouldReturnCorrectOutput()
        {
            using var data = DatabaseMock.Instance;

            await data.Grades.AddAsync(new Grade()
            {
                Id = 23,
                GradeName = "asd",
                GradeSpecialty = "asd",
                TeacherId = 23,
                SchoolId = 2
            });
            await data.SaveChangesAsync();

            await data.Grades.AddAsync(new Grade()
            {
                Id = 3,
                GradeName = "asd",
                GradeSpecialty = "asd",
                TeacherId = 223,
                SchoolId = 2
            });
            await data.SaveChangesAsync();

            await data.Grades.AddAsync(new Grade()
            {
                Id = 2333,
                GradeName = "asd",
                GradeSpecialty = "asd",
                TeacherId = 233,
                SchoolId = 23
            });
            await data.SaveChangesAsync();

            await data.Grades.AddAsync(new Grade()
            {
                Id = 2334,
                GradeName = "asd",
                GradeSpecialty = "asd",
                TeacherId = 23,
                SchoolId = 2
            });
            await data.SaveChangesAsync();


            var service = new GradeService(data, null, MapperMock.MappingData());

            var result = await service.GetGradesBySchoolId(2);

            Assert.Equal(3, result.Count);
        }
    }
}
