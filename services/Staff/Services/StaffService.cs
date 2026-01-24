using Mapster;
using Staff.Models;
using Staff.Repositories;

using Staff.Apis;

namespace Staff.Services;

internal class StaffService(IStaffRepository staffRepository) : IStaffService
{
    public async Task CreateAsync(CreateStaffRequest createStaffRequest, CancellationToken cancellationToken = default)
    {
        await staffRepository.CreateAsync(createStaffRequest.Adapt<StaffModel>(), cancellationToken);
    }

    public async Task<List<StaffModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await staffRepository.GetAllAsync(cancellationToken);
    }

    public async Task<StaffModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await staffRepository.GetByIdAsync(id, cancellationToken);
    }
}
