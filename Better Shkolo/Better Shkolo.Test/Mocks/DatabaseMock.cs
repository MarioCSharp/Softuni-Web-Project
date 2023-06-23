using Better_Shkolo.Data;
using Microsoft.EntityFrameworkCore;

namespace Better_Shkolo.Test.Mocks
{
    public class DatabaseMock
    {
        public static ApplicationDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                                           .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

                return new ApplicationDbContext(dbContextOptions);
            }
        }
    }
}
