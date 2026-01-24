using Microsoft.EntityFrameworkCore;
using Auth.Core.App.Abstractions;
using Auth.Infrastructure.Repositories;
using Auth.Core.App.Services;

namespace Auth.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAccountService, AccountService>();

        return serviceCollection;
    }

    public static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAccountRepository, PSQLAccountRepository>(); 

        serviceCollection.AddDbContext<AccountContext>( x =>
        {
            x.UseNpgsql("Host=db;Database=db_default;Username=postgres;Password=postgres;");    
        });

        return serviceCollection;
    }
}
