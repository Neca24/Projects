using BusinessLayer.Interfaces;
using Entities.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Controllers
{
    [Route("account")]
    public class UserController : Controller
    {
        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm]LoginDTO loginDTO)
        {
            if(loginDTO == null)
            {
                return BadRequest();
            }
            var result = await _userBusiness.Login(loginDTO);

            if(result.Success)
            {
                var role = await _userBusiness.GetRole(loginDTO);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,loginDTO.Email),
                    new Claim(ClaimTypes.Role,role.Name!)
                };

                var claimIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(claimIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(1)
                });

                return Redirect("/");
            }
            else
            {
                return Redirect("/login?error=true");
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/login");
        }
    }
}
