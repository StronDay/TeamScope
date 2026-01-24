using Auth.Core.App.Services;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Apis;

public class AccountController
{   
    [Route("api/Auth")]
    [ApiController]
    public class StaffController(IAccountService staffService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] string userName, [FromBody] string password)
        {
            await staffService.CreateAsync(userName, password);

            return NoContent();
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
