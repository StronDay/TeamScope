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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allStaff = await staffService.GetAllAsync();
            
            // Если список пустой
            if (allStaff.Count == 0)
            {
                return Ok(new 
                {
                    message = "There are no employees in the database",
                    count = 0,
                    data = new List<object>()
                });
            }
            
            // Преобразуем в JSON
            var result = allStaff.Select(staff => new
            {
                id = staff.Id,
                firstName = staff.FirstName,
                lastName = staff.SecondName,
                email = staff.Email,
                age = staff.Age,
                position = staff.JobPosition,
                phone = staff.PhoneNumber,
            }).ToList();
            
            return Ok(new
            {
                count = result.Count,
                data = result
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            // Получаем сотрудника
            var staff = await staffService.GetByIdAsync(id);
            
            // Если не найден - возвращаем 404
            if (staff == null)
            {
                return NotFound(new 
                {
                    error = $"Employee with ID {id} not found"
                });
            }
            
            // Возвращаем сотрудника в формате JSON
            return Ok(new
            {
                id = staff.Id,
                firstName = staff.FirstName,
                lastName = staff.SecondName,
                email = staff.Email,
                age = staff.Age,
                position = staff.JobPosition,
                phone = staff.PhoneNumber
            });
        }
    }
}
