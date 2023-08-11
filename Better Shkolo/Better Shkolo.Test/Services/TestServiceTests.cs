using Better_Shkolo.Models.Test;
using Better_Shkolo.Services.AccountService;
using Better_Shkolo.Services.TestService;
using Better_Shkolo.Test.Mocks;

namespace Better_Shkolo.Test.Services
{
    public class TestServiceTests
    {
        [Fact]
        public async void AddTestShouldWorkCorrectly()
        {
            using var context = DatabaseMock.Instance;

            await context.Subjects.AddAsync(new Subject()
            {
                Id = 23,
                SchoolId = 3,
                GradeId = 4,
                TeacherId = 5,
                Name = "test"
            });
            await context.SaveChangesAsync();

            var model = new TestAddModel()
            {
                SubjectId = 23,
                TestDate = DateTime.Now,
            };

            var accountService = new Mock<IAccountService>();
            var service = new TestService(context, accountService.Object, MapperMock.MappingData());

            var res = await service.Add(model);

            Assert.True(res);
        }

        [Fact]
        public async void AddTestShouldReturnFalse()
        {
            using var context = DatabaseMock.Instance;

            var model = new TestAddModel()
            {
                SubjectId = 23,
                TestDate = DateTime.Now,
            };

            var accountService = new Mock<IAccountService>();
            var service = new TestService(context, accountService.Object, MapperMock.MappingData());

            var res = await service.Add(model);

            Assert.False(res);
        }
    }
}
