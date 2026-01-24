using Microsoft.EntityFrameworkCore;
using Auth.Core.Domain.Models;

namespace Auth.Infrastructure;

/// <remarks>
/// Add migrations using the following command inside the 'Auth' project directory:
///
/// dotnet ef migrations add [migration-name] -c AuthContext -o Infrastructure/Migrations/
/// </remarks>
public class AccountContext : DbContext
{
    public AccountContext(DbContextOptions<AccountContext> options) : base(options)
    {
    }

    public required DbSet<AccountModel> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<AccountModel>().HasKey(x => x.Id);

        base.OnModelCreating(builder);
    }
}
