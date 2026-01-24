using Mapster;
using Staff.Core.App.Abstractions;
using Staff.Core.Domain.Models;

using Staff.Apis.DTOs;

namespace Staff.Core.App.Services;

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
