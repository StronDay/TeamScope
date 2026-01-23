using Microsoft.EntityFrameworkCore;
using Staff.Infrastructure;
using Staff.Repositories;
using Staff.Services;

namespace Staff.Extensions;

public static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IStaffService, StaffService>();

        return serviceCollection;
    }

    public static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IStaffRepository, PSQLStaffRepository>(); 

        serviceCollection.AddDbContext<StaffContext>( x =>
        {
            x.UseNpgsql("Host=db;Database=db_default;Username=postgres;Password=postgres;");    
        });

        return serviceCollection;
    }
}
