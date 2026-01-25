using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Staff.Apis.DTOs;
using Staff.Core.App.Filters;
using Staff.Core.App.Services;

namespace Staff.Apis
{
    // [Route("api/[controller]")]
    [Route("api/Staff")]
    [ApiController]
    public class StaffController(IStaffService staffService) : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateStaffRequest createStaffRequest)
        {
            await staffService.CreateAsync(createStaffRequest);

            return NoContent();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] StaffFilter staffFilter)
        {
            var allStaff = await staffService.GetAllAsync(staffFilter);
            
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

        [Authorize]
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
