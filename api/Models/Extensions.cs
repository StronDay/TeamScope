using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace api.Models;

public static class Extensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<ApiContext>(x =>
        {
            x.UseNpgsql("Host=db;DataBase=db_default;UserName=postgres;Password=postgres");
        });

        return serviceCollection;
    }
}