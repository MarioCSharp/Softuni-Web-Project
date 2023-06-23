using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Absence;
using Better_Shkolo.Services.AbsenceService;
using Better_Shkolo.Test.Mocks;

namespace Better_Shkolo.Test.Services
{
    public class AbsenceServiceTests
    {
        [Fact]
        public async void AddAbsenceShouldReturnFalseWithInvalidSubject()
        {
            using var data = DatabaseMock.Instance;

            var service = new AbsencesService(data);

            var model = new AbsencesAddModel()
            {
                SubjectId = 31,
                SchoolId = 1,
            };

            var result = await service.Add(model);

            Assert.False(result);
        }
        [Fact]
        public async void AddAbsenceShouldReturnFalseWithInvalidStudent()
        {
            using var data = DatabaseMock.Instance;

            var service = new AbsencesService(data);

            var model = new AbsencesAddModel()
            {
                Id = 31,
                SchoolId = 1,
            };

            var result = await service.Add(model);

            Assert.False(result);
        }
        [Fact]
        public async void AddAbsenceShouldReturnFalseWithInvalidSchool()
        {
            using var data = DatabaseMock.Instance;

            var service = new AbsencesService(data);

            var model = new AbsencesAddModel()
            {
                Id = 31,
                SchoolId = 1,
            };

            var result = await service.Add(model);

            Assert.False(result);
        }
        [Fact]
        public async void AddAbsenceShouldReturnTrue()
        {
            using var data = DatabaseMock.Instance;

            data.Students.Add(new Student()
            {
                Id = 22,
                UserId = "asd",
                SchoolId = 2,
                GradeId = 3,
                GradeTeacherId = 32,
                ParentId = 2
            });
            await data.SaveChangesAsync();

            data.Subjects.Add(new Subject()
            {
                Id = 1,
                Name = "Test",
                TeacherId = 3,
                SchoolId = 2,
                GradeId = 3
            });
            await data.SaveChangesAsync();

            data.Schools.Add(new School()
            {
                Id = 2,
                Name = "Test",
                City = "Test",
                DirectorId = "asd"
            });
            await data.SaveChangesAsync();

            var service = new AbsencesService(data);

            var model = new AbsencesAddModel()
            {
                Id = 22,
                SchoolId = 2,
                SubjectId = 1,
            };

            var result = await service.Add(model);

            Assert.True(result);
        }
        [Fact]
        public async void GetAbsencesShouldReturnAllOfStundetsAbsencesStudentView()
        {
            using var data = DatabaseMock.Instance;

            await data.Users.AddAsync(new User() 
            { 
                Id = "teacherId",
                FirstName = "Test",
                LastName = "Test",
            });
            await data.SaveChangesAsync();

            await data.Students.AddAsync(new Student()
            {
                Id = 233,
                UserId = "studentId",
                SchoolId = 1,
                GradeId = 2,
                GradeTeacherId = 3,
            });
            await data.SaveChangesAsync();

            await data.Subjects.AddAsync(new Subject()
            {
                Id = 1,
                Name = "asd",
                TeacherId = 1,
                SchoolId = 1,
                GradeId = 2,
            });
            await data.SaveChangesAsync();

            await data.Subjects.AddAsync(new Subject()
            {
                Id = 3,
                Name = "asd",
                TeacherId = 1,
                SchoolId = 1,
                GradeId = 2,
            });
            await data.SaveChangesAsync();

            await data.Teachers.AddAsync(new Teacher()
            {
                Id = 2,
                SchoolId = 1,
                UserId = "teacherId"
            });
            await data.SaveChangesAsync();

            await data.Absencess.AddAsync(new Absences()
            {
                AddedOn = DateTime.Now,
                SubjectId = 1,
                TeacherId = 2,
                StudentId = 233,
                SchoolId = 1,
            });
            await data.SaveChangesAsync();

            await data.Absencess.AddAsync(new Absences()
            {
                AddedOn = DateTime.Now,
                SubjectId = 3,
                TeacherId = 2,
                StudentId = 233,
                SchoolId = 1,
            });
            await data.SaveChangesAsync();

            await data.Absencess.AddAsync(new Absences()
            {
                AddedOn = DateTime.Now,
                SubjectId = 3,
                TeacherId = 2,
                StudentId = 233,
                SchoolId = 1,
            });
            await data.SaveChangesAsync();

            await data.Absencess.AddAsync(new Absences()
            {
                AddedOn = DateTime.Now,
                SubjectId = 1,
                TeacherId = 2,
                StudentId = 233,
                SchoolId = 1,
            });
            await data.SaveChangesAsync();

            await data.Absencess.AddAsync(new Absences()
            {
                AddedOn = DateTime.Now,
                SubjectId = 3,
                TeacherId = 2,
                StudentId = 233,
                SchoolId = 1,
            });
            await data.SaveChangesAsync();

            var service = new AbsencesService(data);

            var result = await service.GetAbsenceses("studentId");

            Assert.Equal(2, result.Count);
            Assert.Equal(2, result[0].Absenceses.Count);
            Assert.Equal(3, result[1].Absenceses.Count);
        }
    }
}
