using Staff.Models;

namespace Staff.Repositories;

public interface IStaffRepository
{
    Task CreateAsync(StaffModel staffModel, CancellationToken cancellationToken = default);
}
