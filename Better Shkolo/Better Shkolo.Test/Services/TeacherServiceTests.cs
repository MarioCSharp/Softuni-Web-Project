using Better_Shkolo.Models.Teacher;
using Better_Shkolo.Services.TeacherService;
using Better_Shkolo.Test.Mocks;

namespace Better_Shkolo.Test.Services
{
    public class TeacherServiceTests
    {
        [Fact]
        public async void CreateTeacherShouldWorkCorrectly()
        {
            using var data = DatabaseMock.Instance;

            await data.AddAsync(new User()
            {
                Id = "teacherId",
                FirstName = "Test",
                LastName = "Test",
                PasswordHash = "dadasdas"
            });
            await data.SaveChangesAsync();

            var service = new TeacherService(data, UserManagerMock.Instance<User>().Object, null);

            var result = await service.Create(new TeacherCreateModel()
            {
                UserId = "teacherId",
                SchoolId = 13,
            });

            Assert.True(result);
        }

        [Fact]
        public async void DeleteTeacherShouldWorkCorrectly()
        {
            using var data = DatabaseMock.Instance;

            await data.AddAsync(new User()
            {
                Id = "teacherId",
                FirstName = "Test",
                LastName = "Test",
                PasswordHash = "dadasdas"
            });
            await data.SaveChangesAsync();

            var service = new TeacherService(data, UserManagerMock.Instance<User>().Object, null);

            await service.Create(new TeacherCreateModel()
            {
                UserId = "teacherId",
                SchoolId = 13,
            });

            var result = await service.DeleteTeacher(1);

            Assert.True(result);
        }

        [Fact]
        public async void GetTeacherShouldWorkCorrectly()
        {
            using var data = DatabaseMock.Instance;

            var teacher = new Teacher()
            {
                Id = 44,
                SchoolId = 1,
                UserId = "dasdas",
            };
            await data.AddAsync(teacher);
            await data.SaveChangesAsync();

            var service = new TeacherService(data, UserManagerMock.Instance<User>().Object, null);

            var result = await service.GetTeacher(44);

            Assert.Equal(teacher, result);
        }
    }
}
