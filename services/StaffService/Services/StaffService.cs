using StaffService.Models;
using StaffService.Repositories;

namespace StaffService.Services;

internal class StaffService(IStaffRepository staffRepository) : IStaffService
{
    public async Task CreateAsync(string firstName, string secondName, CancellationToken cancellationToken = default)
    {
        var staff = new Staff
        {
            FirstName = firstName,
            SecondName = secondName,
            Age = 23,
            JobPosition = "developer",
            PhoneNumber = 999,
            Email = "simple@simple.com",
        };

        await staffRepository.CreateAsync(staff, cancellationToken);
    }
}
