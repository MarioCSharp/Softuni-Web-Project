using Better_Shkolo.Data.Models;
using Better_Shkolo.Models.Account;
using Better_Shkolo.Services.AccountService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Better_Shkolo.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private IAccountService accountService;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
                                IAccountService accountService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.accountService = accountService;
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var model = new RegisterModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerModel);
            }

            var user = new User
            {
                Email = registerModel.Email,
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                UserName = registerModel.Email,
                EmailConfirmed = true,
                Address = "",
                City = "",
                Country = "",
                Phone = ""
            };

            var result = await userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Something went wrong!");

            return View(registerModel);
        }

        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            var model = new LoginModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            var user = await userManager.FindByEmailAsync(loginModel.Email);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, loginModel.Password, true, false);

                if (result.Succeeded)
                {
                    if (loginModel.ReturnUrl != null)
                    {
                        return Redirect(loginModel.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid login!");

            return View(loginModel);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> MyProfile()
        {
            var user = await accountService.GetUser();

            return View(user);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Information()
        {
            var user = await accountService.GetUser();

            return View(user);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Information(UserProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var res = await accountService.EditUser(model);

            if (!res) return BadRequest();

            return RedirectToAction("MyProfile", "Account");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditAddress()
        {
            var user = await accountService.GetUser();

            var model = new UserAddressModel()
            {
                Address = user.Address,
                City = user.City,
                Country = user.Country,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return View(model);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditAddress(UserAddressModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await accountService.EditAddress(model);

            if (!result) return BadRequest();

            return RedirectToAction(nameof(MyProfile));
        }
    }
}
