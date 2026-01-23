using Microsoft.EntityFrameworkCore;
using Staff.Models;

namespace Staff.Infrastructure;

/// <remarks>
/// Add migrations using the following command inside the 'Staff' project directory:
///
/// dotnet ef migrations add [migration-name] -c StaffContext -o Infrastructure/Migrations/
/// </remarks>
public class StaffContext : DbContext
{
    public StaffContext(DbContextOptions<StaffContext> options) : base(options)
    {
    }

    public required DbSet<StaffModel> Staff { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<StaffModel>().HasKey(x => x.Id);
        builder.Entity<StaffModel>().Property(x => x.Email).HasMaxLength(20);

        base.OnModelCreating(builder);
    }
}
