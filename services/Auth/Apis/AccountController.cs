using Auth.Core.App.Services;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Apis
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController(IAccountService staffService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync(string userName, string password)
        {
            await staffService.CreateAsync(userName, password);

            return NoContent();
        }

        [HttpGet]
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

        [HttpGet("{userName}")]
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

