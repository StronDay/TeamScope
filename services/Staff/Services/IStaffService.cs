using Staff.Models;
using Staff.Apis;

namespace Staff.Services;

public interface IStaffService
{
    Task CreateAsync(CreateStaffRequest createStaffRequest, CancellationToken cancellationToken = default);
    Task<List<StaffModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<StaffModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
