using Staff.Core.Domain.Models;
using Staff.Apis.DTOs;
using Staff.Core.App.Filters;

namespace Staff.Core.App.Services;

public interface IStaffService
{
    Task CreateAsync(CreateStaffRequest createStaffRequest, CancellationToken cancellationToken = default);
    Task<List<StaffModel>> GetAllAsync(StaffFilter staffFilter, CancellationToken cancellationToken = default);
    Task<StaffModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
