using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Staff.Services;

namespace Staff.Controllers
{
    // [Route("api/[controller]")]
    [Route("api/Staff")]
    [ApiController]
    public class StaffController(IStaffService staffService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync(string firstName, string secondName)
        {
            await staffService.CreateAsync(firstName, secondName);

            return NoContent();
        }
    }
}
