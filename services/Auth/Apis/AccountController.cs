using Auth.Core.App.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Apis
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController(IAccountService staffService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(string userName, string password)
        {
            await staffService.RegisterAsync(userName, password);

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var jwtToken = await staffService.Login(userName, password);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                // Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(3)
            };

            Response.Cookies.Append("jwt_token", jwtToken, cookieOptions);

            return Ok(jwtToken);
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                // Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(-1)
            };

            Response.Cookies.Delete("jwt_token");

            return Ok(new { message = "Logged out successfully" });
        }

        [HttpGet("check-cookies")]
        public IActionResult CheckCookies()
        {
            var cookies = Request.Cookies.Select(c => new 
            { 
                Name = c.Key, 
                Value = c.Key == "jwt_token" ? "[HIDDEN]" : c.Value,
                Length = c.Value?.Length ?? 0
            }).ToList();
            
            return Ok(new 
            {
                message = "Current cookies",
                cookies = cookies,
                count = cookies.Count
            });
        }

        [Authorize]
        [HttpGet("account")]
        public async Task<IActionResult> GetAllAsync()
        {
            var accounst = await staffService.GetAllAsync();
            
            if (accounst.Count == 0)
            {
                return Ok(new 
                {
                    message = "There are no employees in the database",
                    count = 0,
                    data = new List<object>()
                });
            }
            
            return Ok(new
            {
                count = accounst.Count,
                data = accounst
            });
        }

        [HttpGet("account/{userName}")]
        public async Task<IActionResult> GetByUserNameAsync(string userName)
        {
            var account = await staffService.GetByUserNameAsync(userName);
            
            if (account == null)
            {
                return NotFound(new 
                {
                    error = $"Account with user name {userName} not found"
                });
            }

            return Ok(userName);
        }
    }
}

