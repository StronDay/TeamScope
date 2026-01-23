using StaffService.Models;

namespace StaffService.Repositories;

public interface IStaffRepository
{
    Task CreateAsync(Staff staff, CancellationToken cancellationToken = default);
}
