using Better_Shkolo.Models.Mark;
using Better_Shkolo.Services.MarkService;
using Better_Shkolo.Test.Mocks;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Better_Shkolo.Test.Services
{
    public class MarkServiceTests
    {
        [Fact]
        public async void AddShouldReturnFalseBecauseOfNullTeacher()
        {
            using var context = DatabaseMock.Instance;

            await context.Students.AddAsync(new Student()
            {
                Id = 23,
                UserId = "studentID",
                SchoolId = 3,
                GradeId = 4,
                GradeTeacherId = 5,
            });
            await context.SaveChangesAsync();

            await context.Subjects.AddAsync(new Subject()
            {
                Id = 32,
                Name = "Test",
                TeacherId = 6,
                SchoolId = 3,
                GradeId = 4,
            });
            await context.SaveChangesAsync();

            var service = new MarkService(context, TeacherServiceMock.Instance);

            var result = await service.Add(new MarkAddModel()
            {
                Value = 3,
                SubjectId = 32,
                StudentId = 23
            }, 32, "asd asfasfasf");

            Assert.False(result);
        }

        [Fact]
        public async void AddShouldReturnFalseBecauseOfNullSubject()
        {
            using var context = DatabaseMock.Instance;

            await context.Students.AddAsync(new Student()
            {
                Id = 23,
                UserId = "studentID",
                SchoolId = 3,
                GradeId = 4,
                GradeTeacherId = 5,
            });
            await context.SaveChangesAsync();

            await context.Teachers.AddAsync(new Teacher()
            {
                Id = 44,
                SchoolId = 3,
                UserId = "asdasdasdasd"
            });
            await context.SaveChangesAsync();

            var service = new MarkService(context, TeacherServiceMock.Instance);

            var result = await service.Add(new MarkAddModel()
            {
                Value = 3,
                SubjectId = 32,
                StudentId = 23
            }, 32, "asdasdasdasd");

            Assert.False(result);
        }
        [Fact]
        public async void AddShouldReturnFalseBecauseOfNullStudent()
        {
            using var context = DatabaseMock.Instance;

            await context.Subjects.AddAsync(new Subject()
            {
                Id = 32,
                Name = "asddas",
                TeacherId = 44,
                SchoolId = 3,
                GradeId = 4,
            });
            await context.SaveChangesAsync();

            await context.Teachers.AddAsync(new Teacher()
            {
                Id = 44,
                SchoolId = 3,
                UserId = "asdasdasdasd"
            });
            await context.SaveChangesAsync();

            var service = new MarkService(context, TeacherServiceMock.Instance);

            var result = await service.Add(new MarkAddModel()
            {
                Value = 3,
                SubjectId = 32,
                StudentId = 23
            }, 32, "asdasdasdasd");

            Assert.False(result);
        }

        [Fact]
        public async void AddShouldReturnFalseBecauseOfInvalidValue()
        {
            using var context = DatabaseMock.Instance;

            await context.Subjects.AddAsync(new Subject()
            {
                Id = 32,
                Name = "asddas",
                TeacherId = 44,
                SchoolId = 3,
                GradeId = 4,
            });
            await context.SaveChangesAsync();

            await context.Teachers.AddAsync(new Teacher()
            {
                Id = 44,
                SchoolId = 3,
                UserId = "asdasdasdasd"
            });
            await context.SaveChangesAsync();

            await context.Students.AddAsync(new Student()
            {
                Id = 23,
                UserId = "studentID",
                SchoolId = 3,
                GradeId = 4,
                GradeTeacherId = 5,
            });
            await context.SaveChangesAsync();

            var service = new MarkService(context, TeacherServiceMock.Instance);

            var result = await service.Add(new MarkAddModel()
            {
                Value = 1,
                SubjectId = 32,
                StudentId = 23
            }, 32, "asdasdasdasd");

            Assert.False(result);
        }

        [Fact]
        public async void AddShouldReturnTrue()
        {
            using var context = DatabaseMock.Instance;

            await context.Subjects.AddAsync(new Subject()
            {
                Id = 32,
                Name = "asddas",
                TeacherId = 44,
                SchoolId = 3,
                GradeId = 4,
            });
            await context.SaveChangesAsync();

            await context.Teachers.AddAsync(new Teacher()
            {
                Id = 44,
                SchoolId = 3,
                UserId = "asdasdasdasd"
            });
            await context.SaveChangesAsync();

            await context.Students.AddAsync(new Student()
            {
                Id = 23,
                UserId = "studentID",
                SchoolId = 3,
                GradeId = 4,
                GradeTeacherId = 5,
            });
            await context.SaveChangesAsync();

            var service = new MarkService(context, TeacherServiceMock.Instance);

            var result = await service.Add(new MarkAddModel()
            {
                Value = 3,
                SubjectId = 32,
                StudentId = 23
            }, 32, "asdasdasdasd");

            Assert.True(result);
        }
    }
}
