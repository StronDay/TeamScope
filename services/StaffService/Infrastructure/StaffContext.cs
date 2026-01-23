using Microsoft.EntityFrameworkCore;
using StaffService.Models;

namespace StaffService.Infrastructure;

/// <remarks>
/// Для добавления миграций выполните следующую команду 
/// в директории проекта 'StaffService':
///
/// Команда: dotnet ef migrations add --context StaffContext [имя-миграции]
/// 
/// Пример: dotnet ef migrations add --context StaffContext InitialCreate
/// 
/// Контекст: StaffContext - имя класса DbContext
/// </remarks>
public class StaffContext : DbContext
{
    public StaffContext(DbContextOptions<StaffContext> options) : base(options)
    {
    }

    public required DbSet<Staff> Staff { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Staff>().HasKey(x => x.Id);
        builder.Entity<Staff>().Property(x => x.Email).HasMaxLength(20);

        base.OnModelCreating(builder);
    }
}
