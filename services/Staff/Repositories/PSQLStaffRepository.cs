using Staff.Infrastructure;
using Staff.Models;

namespace Staff.Repositories;

internal class PSQLStaffRepository(StaffContext context) : IStaffRepository
{
    public async Task CreateAsync(StaffModel staffModel, CancellationToken cancellationToken = default)
    {
        await context.Staff.AddAsync(staffModel, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
