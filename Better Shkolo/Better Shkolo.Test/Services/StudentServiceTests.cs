using AutoMapper;
using Better_Shkolo.Models.Parent;
using Better_Shkolo.Models.Student;
using Better_Shkolo.Services.StudentService;
using Better_Shkolo.Test.Mocks;

namespace Better_Shkolo.Test.Services
{
    public class StudentServiceTests
    {
        [Fact]
        public async void AddStudentShouldWorkCorrectly()
        {
            using var data = DatabaseMock.Instance;

            await data.AddAsync(new User()
            {
                FirstName = "Test",
                LastName = "Test",
                Id = "test",
                EmailConfirmed = true,
                PasswordHash = "asdasfas",
            });
            await data.SaveChangesAsync();

            await data.AddAsync(new School()
            {
                Id = 13,
                Name = "Test",
                City = "Test",
                DirectorId = "Test",
            });
            await data.SaveChangesAsync();

            await data.AddAsync(new Grade()
            {
                Id = 44,
                GradeName = "Test",
                GradeSpecialty = "Test",
                TeacherId = 1,
                SchoolId = 13,
            });
            await data.SaveChangesAsync();

            await data.AddAsync(new Teacher()
            {
                Id = 1,
                SchoolId = 13,
                UserId = "Test",
            });
            await data.SaveChangesAsync();

            var service = new StudentService(data, UserManagerMock.Instance<User>().Object, MapperMock.MappingData());

            var result = await service.Add(new StudentCreateModel()
            {
                UserId = "test",
                SchoolId = 13,
                GradeId = 44,
                GradeTeacherId = 1
            });

            Assert.True(result);
        }

        [Fact]
        public async void AddStudentShouldReturnFalseBecauseOfNullStudent()
        {
            using var data = DatabaseMock.Instance;

            await data.AddAsync(new School()
            {
                Id = 13,
                Name = "Test",
                City = "Test",
                DirectorId = "Test",
            });
            await data.SaveChangesAsync();

            await data.AddAsync(new Grade()
            {
                Id = 44,
                GradeName = "Test",
                GradeSpecialty = "Test",
                TeacherId = 1,
                SchoolId = 13,
            });
            await data.SaveChangesAsync();

            await data.AddAsync(new Teacher()
            {
                Id = 1,
                SchoolId = 13,
                UserId = "Test",
            });
            await data.SaveChangesAsync();

            var service = new StudentService(data, UserManagerMock.Instance<User>().Object, MapperMock.MappingData());

            var result = await service.Add(new StudentCreateModel()
            {
                UserId = "test",
                SchoolId = 13,
                GradeId = 44,
                GradeTeacherId = 1
            });

            Assert.False(result);
        }
        [Fact]
        public async void AddStudentShouldReturnFalseBecauseOfNullSchool()
        {
            using var data = DatabaseMock.Instance;

            await data.AddAsync(new User()
            {
                FirstName = "Test",
                LastName = "Test",
                Id = "test",
                EmailConfirmed = true,
                PasswordHash = "asdasfas",
            });
            await data.SaveChangesAsync();

            await data.AddAsync(new Grade()
            {
                Id = 44,
                GradeName = "Test",
                GradeSpecialty = "Test",
                TeacherId = 1,
                SchoolId = 13,
            });
            await data.SaveChangesAsync();

            await data.AddAsync(new Teacher()
            {
                Id = 1,
                SchoolId = 13,
                UserId = "Test",
            });
            await data.SaveChangesAsync();

            var service = new StudentService(data, UserManagerMock.Instance<User>().Object, MapperMock.MappingData());

            var result = await service.Add(new StudentCreateModel()
            {
                UserId = "test",
                SchoolId = 13,
                GradeId = 44,
                GradeTeacherId = 1
            });

            Assert.False(result);
        }

        [Fact]
        public async void AddStudentShouldReturnFalseBecauseOfNullGrade()
        {
            using var data = DatabaseMock.Instance;

            await data.AddAsync(new User()
            {
                FirstName = "Test",
                LastName = "Test",
                Id = "test",
                EmailConfirmed = true,
                PasswordHash = "asdasfas",
            });
            await data.SaveChangesAsync();

            await data.AddAsync(new School()
            {
                Id = 13,
                Name = "Test",
                City = "Test",
                DirectorId = "Test",
            });
            await data.SaveChangesAsync();

            await data.AddAsync(new Teacher()
            {
                Id = 1,
                SchoolId = 13,
                UserId = "Test",
            });
            await data.SaveChangesAsync();

            var service = new StudentService(data, UserManagerMock.Instance<User>().Object, MapperMock.MappingData());

            var result = await service.Add(new StudentCreateModel()
            {
                UserId = "test",
                SchoolId = 13,
                GradeId = 44,
                GradeTeacherId = 1
            });

            Assert.False(result);
        }

        [Fact]
        public async void AddStudentShouldReturnFalseBecauseOfNullTeacher()
        {
            using var data = DatabaseMock.Instance;

            await data.AddAsync(new User()
            {
                FirstName = "Test",
                LastName = "Test",
                Id = "test",
                EmailConfirmed = true,
                PasswordHash = "asdasfas",
            });
            await data.SaveChangesAsync();

            await data.AddAsync(new School()
            {
                Id = 13,
                Name = "Test",
                City = "Test",
                DirectorId = "Test",
            });
            await data.SaveChangesAsync();

            await data.AddAsync(new Grade()
            {
                Id = 44,
                GradeName = "Test",
                GradeSpecialty = "Test",
                TeacherId = 1,
                SchoolId = 13,
            });
            await data.SaveChangesAsync();

            var service = new StudentService(data, UserManagerMock.Instance<User>().Object, MapperMock.MappingData());

            var result = await service.Add(new StudentCreateModel()
            {
                UserId = "test",
                SchoolId = 13,
                GradeId = 44,
                GradeTeacherId = 1
            });

            Assert.False(result);
        }

        [Fact]
        public async void AsignParentShouldWorkCorrectly()
        {
            using var data = DatabaseMock.Instance;

            await data.Users.AddAsync(new User()
            {
                Id = "studentId",
                FirstName = "test",
                LastName = "Test",
                PasswordHash = "asdasdas"
            });
            await data.SaveChangesAsync();

            await data.Users.AddAsync(new User()
            {
                Id = "parentId",
                FirstName = "test",
                LastName = "Test",
                PasswordHash = "asdasdas"
            });
            await data.SaveChangesAsync();

            await data.Students.AddAsync(new Student()
            {
                Id = 3,
                UserId = "studentId",
                SchoolId = 13,
                GradeId = 32,
                GradeTeacherId = 31,
            });
            await data.SaveChangesAsync();

            var service = new StudentService(data, UserManagerMock.Instance<User>().Object, MapperMock.MappingData());

            var result = await service.AsignParent(new ParentCreateModel
            {
                UserId = "parentId"
            }, 3);

            Assert.True(result);
        }

        [Fact]
        public async void AsignParentShouldReturnFalseBecauseOfNullStudent()
        {
            using var data = DatabaseMock.Instance;

            await data.Users.AddAsync(new User()
            {
                Id = "studentId",
                FirstName = "test",
                LastName = "Test",
                PasswordHash = "asdasdas"
            });
            await data.SaveChangesAsync();

            await data.Users.AddAsync(new User()
            {
                Id = "parentId",
                FirstName = "test",
                LastName = "Test",
                PasswordHash = "asdasdas"
            });
            await data.SaveChangesAsync();

            var service = new StudentService(data, UserManagerMock.Instance<User>().Object, MapperMock.MappingData());

            var result = await service.AsignParent(new ParentCreateModel
            {
                UserId = "parentId"
            }, 3);

            Assert.False(result);
        }

        [Fact]
        public async void DeleteStudentShouldWorkCorrectly()
        {
            using var data = DatabaseMock.Instance;

            await data.AddAsync(new User()
            {
                FirstName = "Test",
                LastName = "Test",
                Id = "test",
                EmailConfirmed = true,
                PasswordHash = "asdasfas",
            });
            await data.SaveChangesAsync();

            await data.AddAsync(new School()
            {
                Id = 13,
                Name = "Test",
                City = "Test",
                DirectorId = "Test",
            });
            await data.SaveChangesAsync();

            await data.AddAsync(new Grade()
            {
                Id = 44,
                GradeName = "Test",
                GradeSpecialty = "Test",
                TeacherId = 1,
                SchoolId = 13,
            });
            await data.SaveChangesAsync();

            await data.AddAsync(new Teacher()
            {
                Id = 1,
                SchoolId = 13,
                UserId = "Test",
            });
            await data.SaveChangesAsync();

            var service = new StudentService(data, UserManagerMock.Instance<User>().Object, MapperMock.MappingData());

            await service.Add(new StudentCreateModel()
            {
                UserId = "test",
                SchoolId = 13,
                GradeId = 44,
                GradeTeacherId = 1
            });

            var result = await service.Delete(1);

            Assert.True(result);
        }

        [Fact]
        public async void DeleteStudentShouldReturnFalseBecauseOfInvalidStudent()
        {
            using var data = DatabaseMock.Instance;

            await data.AddAsync(new User()
            {
                FirstName = "Test",
                LastName = "Test",
                Id = "test",
                EmailConfirmed = true,
                PasswordHash = "asdasfas",
            });
            await data.SaveChangesAsync();

            await data.AddAsync(new School()
            {
                Id = 13,
                Name = "Test",
                City = "Test",
                DirectorId = "Test",
            });
            await data.SaveChangesAsync();

            await data.AddAsync(new Grade()
            {
                Id = 44,
                GradeName = "Test",
                GradeSpecialty = "Test",
                TeacherId = 1,
                SchoolId = 13,
            });
            await data.SaveChangesAsync();

            await data.AddAsync(new Teacher()
            {
                Id = 1,
                SchoolId = 13,
                UserId = "Test",
            });
            await data.SaveChangesAsync();

            var service = new StudentService(data, UserManagerMock.Instance<User>().Object, MapperMock.MappingData());

            await service.Add(new StudentCreateModel()
            {
                UserId = "test",
                SchoolId = 13,
                GradeId = 44,
                GradeTeacherId = 1
            });

            var result = await service.Delete(2);

            Assert.False(result);
        }

        [Fact]
        public async void GetStudentShouldWorkCorrectly()
        {
            using var data = DatabaseMock.Instance;

            var student = new Student()
            {
                Id = 312,
                UserId = "test",
                SchoolId = 13,
                GradeId = 33,
                GradeTeacherId = 334,
            };

            await data.AddAsync(student);
            await data.SaveChangesAsync();

            var service = new StudentService(data, UserManagerMock.Instance<User>().Object, MapperMock.MappingData());


            var result = await service.GetStudent(312);

            Assert.Equal(student, result);
        }
    }
}
