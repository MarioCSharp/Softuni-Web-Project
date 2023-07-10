using Better_Shkolo.Services.SchoolService;
using Better_Shkolo.Test.Mocks;

namespace Better_Shkolo.Test.Services
{
    public class SchoolServiceTests
    {
        [Fact]
        public async void AddSchoolShouldWorkCorrectly()
        {
            using var data = DatabaseMock.Instance;

            await data.Users.AddAsync(new User()
            {
                FirstName = "as",
                LastName = "d",
                Id = "asd",
                PasswordHash = "asdasdas"
            });
            await data.SaveChangesAsync();

            var school = new School()
            {
                Id = 1,
                Name = "asdasfasfs",
                City = "ffga",
                DirectorId = "asd"
            };

            var service = new SchoolService(data, UserManagerMock.Instance<User>().Object);

            var res = await service.AddSchool(school);

            Assert.True(res);
        }

        [Fact]
        public async void DeleteSchoolShouldReturnFalseWithInvalidSchool()
        {
            using var data = DatabaseMock.Instance;

            await data.Users.AddAsync(new User()
            {
                FirstName = "as",
                LastName = "d",
                Id = "asd",
                PasswordHash = "asdasdas"
            });
            await data.SaveChangesAsync();

            var school = new School()
            {
                Id = 1,
                Name = "asdasfasfs",
                City = "ffga",
                DirectorId = "asd"
            };

            await data.Schools.AddAsync(school);
            await data.SaveChangesAsync();

            var service = new SchoolService(data, UserManagerMock.Instance<User>().Object);

            var res = await service.DeleteSchool(school.Id + 1);

            Assert.False(res);
        }

        [Fact]
        public async void DeleteSchoolShouldWorkCorrectly()
        {
            using var data = DatabaseMock.Instance;

            await data.Users.AddAsync(new User()
            {
                FirstName = "as",
                LastName = "d",
                Id = "asd",
                PasswordHash = "asdasdas"
            });
            await data.SaveChangesAsync();

            var school = new School()
            {
                Id = 1,
                Name = "asdasfasfs",
                City = "ffga",
                DirectorId = "asd"
            };


            var service = new SchoolService(data, UserManagerMock.Instance<User>().Object);

            await service.AddSchool(school);

            var res = await service.DeleteSchool(school.Id);

            Assert.True(res);
        }

        [Fact]
        public async void GetSchoolWorksCorrectly()
        {
            using var data = DatabaseMock.Instance;

            await data.Users.AddAsync(new User()
            {
                FirstName = "as",
                LastName = "d",
                Id = "asd",
                PasswordHash = "asdasdas"
            });
            await data.SaveChangesAsync();

            await data.Users.AddAsync(new User()
            {
                FirstName = "as",
                LastName = "d",
                Id = "bsd",
                PasswordHash = "asdasdas"
            });
            await data.SaveChangesAsync();

            await data.Users.AddAsync(new User()
            {
                FirstName = "as",
                LastName = "d",
                Id = "csd",
                PasswordHash = "asdasdas"
            });
            await data.SaveChangesAsync();

            var school = new School()
            {
                Id = 1,
                Name = "asdasfasfs",
                City = "ffga",
                DirectorId = "asd"
            };

            var school1 = new School()
            {
                Id = 2,
                Name = "dddd",
                City = "ffafga",
                DirectorId = "bsd"
            };

            var school2 = new School()
            {
                Id = 13,
                Name = "fff",
                City = "ffddga",
                DirectorId = "csd"
            };


            var service = new SchoolService(data, UserManagerMock.Instance<User>().Object);

            await service.AddSchool(school);
            await service.AddSchool(school1);
            await service.AddSchool(school2);

            var res = await service.GetSchool(13);

            Assert.Equal(school2, res);
        }

        [Fact]
        public async void GetAllSchoolsShouldWorkCorrectly()
        {
            using var data = DatabaseMock.Instance;

            await data.Users.AddAsync(new User()
            {
                FirstName = "as",
                LastName = "d",
                Id = "asd",
                PasswordHash = "asdasdas"
            });
            await data.SaveChangesAsync();

            await data.Users.AddAsync(new User()
            {
                FirstName = "as",
                LastName = "d",
                Id = "bsd",
                PasswordHash = "asdasdas"
            });
            await data.SaveChangesAsync();

            await data.Users.AddAsync(new User()
            {
                FirstName = "as",
                LastName = "d",
                Id = "csd",
                PasswordHash = "asdasdas"
            });
            await data.SaveChangesAsync();

            var school = new School()
            {
                Id = 1,
                Name = "asdasfasfs",
                City = "ffga",
                DirectorId = "asd"
            };

            var school1 = new School()
            {
                Id = 2,
                Name = "dddd",
                City = "ffafga",
                DirectorId = "bsd"
            };

            var school2 = new School()
            {
                Id = 13,
                Name = "fff",
                City = "ffddga",
                DirectorId = "csd"
            };


            var service = new SchoolService(data, UserManagerMock.Instance<User>().Object);

            await service.AddSchool(school);
            await service.AddSchool(school1);
            await service.AddSchool(school2);

            var res = await service.GetAllSchools();

            Assert.Equal(3, res.Count);
        }
    }
}
