using StaffService.Models;

namespace StaffService.Repositories;

public interface IStaffRepository
{
    Task<Staff> CreateAsync(Staff staff, CancellationToken cancellationToken = default);
}
