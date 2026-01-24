using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Staff.Services;

namespace Staff.Apis
{
    // [Route("api/[controller]")]
    [Route("api/Staff")]
    [ApiController]
    public class StaffController(IStaffService staffService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateStaffRequest createStaffRequest)
        {
            await staffService.CreateAsync(createStaffRequest);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allStaff = await staffService.GetAllAsync();
            
            if (allStaff.Count == 0)
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
                count = allStaff.Count,
                data = allStaff.Adapt<List<GetStaffRespoce>>()
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var staff = await staffService.GetByIdAsync(id);
            
            if (staff == null)
            {
                return NotFound(new 
                {
                    error = $"Employee with ID {id} not found"
                });
            }

            return Ok(staff.Adapt<GetStaffRespoce>());
        }
    }
}
