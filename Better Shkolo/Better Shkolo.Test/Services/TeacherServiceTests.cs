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
    }
}
