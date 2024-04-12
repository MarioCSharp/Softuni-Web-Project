using BetterShkolo.Data;
using BetterShkolo.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BetterShkolo.Controllers.Api
{
    [ApiController]
    [Route("api/account")]
    public class ShkoloAccountApiController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        public ShkoloAccountApiController(ApplicationDbContext context,
                                   UserManager<User> userManager,
                                   SignInManager<User> signInManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return Unauthorized();
            }

            Url.Content("~/");

            var user = await userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, password, true, false);

                if (result.Succeeded)
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("Register")]
        public async Task<IActionResult> Register(string firstName, string lastName, string email,
                                                  string password)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName)
                || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return Unauthorized();
            }

            var user = new User
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                UserName = email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);

                return Ok(result);
            }

            return BadRequest();
        }
    }
}
