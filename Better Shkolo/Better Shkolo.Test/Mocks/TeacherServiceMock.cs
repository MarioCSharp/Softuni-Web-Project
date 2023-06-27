using Better_Shkolo.Data;
using Better_Shkolo.Services.TeacherService;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Test.Mocks
{
    public class TeacherServiceMock
    {
        public static ITeacherService Instance
        {
            get
            {
                var mock = new Mock<ITeacherService>();

                return mock.Object;
            }
        }
    }
}
