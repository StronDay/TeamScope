using Mapster;
using Staff.Core.App.Abstractions;
using Staff.Core.Domain.Models;

using Staff.Apis.DTOs;
using Staff.Core.App.Filters;

namespace Staff.Core.App.Services;

internal class StaffService(IStaffRepository staffRepository) : IStaffService
{
    public async Task CreateAsync(CreateStaffRequest createStaffRequest, CancellationToken cancellationToken = default)
    {
        await staffRepository.CreateAsync(createStaffRequest.Adapt<StaffModel>(), cancellationToken);
    }

    public async Task<List<StaffModel>> GetAllAsync(StaffFilter staffFilter, CancellationToken cancellationToken = default)
    {
        return await staffRepository.GetAllAsync(staffFilter, cancellationToken);
    }

    public async Task<StaffModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await staffRepository.GetByIdAsync(id, cancellationToken);
    }
}
