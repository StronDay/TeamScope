using Staff.Core.Domain.Models;
using Staff.Apis.DTOs;

namespace Staff.Core.App.Services;

public interface IStaffService
{
    Task CreateAsync(CreateStaffRequest createStaffRequest, CancellationToken cancellationToken = default);
    Task<List<StaffModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<StaffModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
