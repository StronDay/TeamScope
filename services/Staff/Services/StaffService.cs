using Staff.Models;
using Staff.Repositories;

namespace Staff.Services;

internal class StaffService(IStaffRepository staffRepository) : IStaffService
{
    public async Task CreateAsync(string firstName, string secondName, CancellationToken cancellationToken = default)
    {
        var staffModel = new StaffModel
        {
            FirstName = firstName,
            SecondName = secondName,
            Age = 23,
            JobPosition = "developer",
            PhoneNumber = 999,
            Email = "simple@simple.com",
        };

        await staffRepository.CreateAsync(staffModel, cancellationToken);
    }
}
