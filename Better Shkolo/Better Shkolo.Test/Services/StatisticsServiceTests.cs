using Better_Shkolo.Services.StatisticsService;
using Better_Shkolo.Test.Mocks;

namespace Better_Shkolo.Test.Services
{
    public class StatisticsServiceTests
    {
        [Fact]
        public async void GetMarkByIdShouldWorkCorrectly()
        {
            using var data = DatabaseMock.Instance;

            var mark = new Mark() 
            { 
                Id = 33,
                AddedOn = DateTime.UtcNow,
                TeacherId = 32,
                StudentId = 3,
                SchoolId = 4,
                SubjectId = 445,
                Value = 4
            };

            await data.Marks.AddAsync(mark);
            await data.SaveChangesAsync();

            await data.Marks.AddAsync(new Mark()
            {
                Id = 35,
                AddedOn = DateTime.UtcNow,
                TeacherId = 33,
                StudentId = 3,
                SchoolId = 4,
                SubjectId = 445,
                Value = 6
            });
            await data.SaveChangesAsync();

            await data.Teachers.AddAsync(new Teacher()
            {
                Id = 32,
                SchoolId = 4,
                UserId = "teacher"
            });
            await data.SaveChangesAsync();

            await data.Users.AddAsync(new User()
            {
                Id = "teacher",
                PasswordHash = "ds",
                FirstName = "test",
                LastName = "ttt"
            });
            await data.SaveChangesAsync();

            var service = new StatisticsService(data, MemoryCacheMock.Instance.Object);

            var result = await service.GetMarkById(33);

            Assert.Equal(result.AddedOn, mark.AddedOn.ToString("MM/dd/yyyy HH:mm:ss"));
            Assert.Equal(result.Value, mark.Value);
        }
    }
}
