using Staff.Models;

namespace Staff.Services;

public interface IStaffService
{
    Task CreateAsync(string firstName, string secondName, CancellationToken cancellationToken = default);
    Task<List<StaffModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<StaffModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
