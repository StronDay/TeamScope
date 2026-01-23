using StaffService.Infrastructure;
using StaffService.Models;

namespace StaffService.Repositories;

internal class PSQLStaffRepository(StaffContext context) : IStaffRepository
{
    public async Task CreateAsync(Staff staff, CancellationToken cancellationToken = default)
    {
        await context.Staff.AddAsync(staff, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
