using Staff.Core.Domain.Models;

namespace Staff.Core.App.Abstractions;

public interface IStaffRepository
{
    Task CreateAsync(StaffModel staffModel, CancellationToken cancellationToken = default);
    Task<StaffModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<List<StaffModel>> GetAllAsync(CancellationToken cancellationToken = default);
}
