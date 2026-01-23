using Staff.Models;
using Staff.Infrastructure;

namespace Staff.Repositories;

public interface IStaffRepository
{
    Task CreateAsync(StaffModel staffModel, CancellationToken cancellationToken = default);

    Task<StaffModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
