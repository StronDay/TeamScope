using Microsoft.EntityFrameworkCore;
using Staff.Core.App.Abstractions;
using Staff.Core.App.Filters;
using Staff.Core.Domain.Models;

namespace Staff.Infrastructure.Repositories;

internal class PSQLStaffRepository(StaffContext context) : IStaffRepository
{
    public async Task CreateAsync(StaffModel staffModel, CancellationToken cancellationToken = default)
    {
        await context.Staff.AddAsync(staffModel, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<StaffModel>> GetAllAsync(StaffFilter staffFilter, CancellationToken cancellationToken = default)
    {
        return await context.Staff.Filter(staffFilter).ToListAsync(cancellationToken);
    }

    public async Task<StaffModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Staff.FirstOrDefaultAsync(x => x.Id == id, cancellationToken); 
    }
}
