using Microsoft.AspNetCore.Identity;
namespace Better_Shkolo.Test.Mocks
{
    public class UserManagerMock
    {
        public static Mock<UserManager<TUser>> Instance<TUser>()
        where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var passwordHasher = new Mock<IPasswordHasher<TUser>>();

            IList<IUserValidator<TUser>> userValidators = new List<IUserValidator<TUser>>
            {
                new UserValidator<TUser>()
            };
            IList<IPasswordValidator<TUser>> passwordValidators = new List<IPasswordValidator<TUser>>
            {
                new PasswordValidator<TUser>()
            };

            userValidators.Add(new UserValidator<TUser>());
            passwordValidators.Add(new PasswordValidator<TUser>());
            var userManager = new Mock<UserManager<TUser>>(store.Object, null, passwordHasher.Object, userValidators, passwordValidators, null, null, null, null);
            return userManager;
        }
    }
}
