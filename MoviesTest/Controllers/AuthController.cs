using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesTest.Models;
using MoviesTest.ViewModels;
using System.Reflection.Metadata;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace MoviesTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly MovieTestContext _context;
        private readonly string _key = "SuperDuperRandomNumberForRealForReal123";


        public AuthController(MovieTestContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginVM login)
        {
            try
            {
                var user = await _context.Users.Where(x => x.EmailAddress == login.Username)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    var loggedIn = await UserSignIn(user, false);

                    if (loggedIn)
                    {
                        return Ok(new { Message = "Login successful" });
                    }
                }
                return Unauthorized();                
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok(new { Message = "Logout successful" });
        }

        private async Task<bool> UserSignIn(User user, bool rememberMe)
        {
            var claims = new List<Claim>
            {
                //new Claim(ClaimTypes.Role, RoleString)
                new Claim(ClaimTypes.Name, user.Fullname),
                new Claim(ClaimTypes.Email, user.EmailAddress),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(100),
                IsPersistent = rememberMe,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);


            return true;
        }

    }
}
