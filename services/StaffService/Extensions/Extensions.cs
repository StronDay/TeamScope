using Microsoft.EntityFrameworkCore;
using StaffService.Infrastructure;

namespace StaffService.Extensions;

public static class Extensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<StaffContext>( x =>
        {
            x.UseNpgsql("Host=db;Database=db_default;Username=postgres;Password=postgres;");    
        });

        return serviceCollection;
    }
}
