using Microsoft.EntityFrameworkCore;
using Staff.Core.App.Abstractions;
using Staff.Core.App.Services;
using Staff.Infrastructure.Repositories;

namespace Staff.Infrastructure;

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
