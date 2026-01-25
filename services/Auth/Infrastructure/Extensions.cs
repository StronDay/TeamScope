using Microsoft.EntityFrameworkCore;
using Auth.Core.App.Abstractions;
using Auth.Infrastructure.Repositories;
using Auth.Core.App.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Auth.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAccountService, AccountService>();
        serviceCollection.AddScoped<JwtService>();

        return serviceCollection;
    }

    public static IServiceCollection AddAuth(this IServiceCollection serviceCollection, IConfiguration config)
    {
        var authSetting = config.GetSection(nameof(AuthSettings))
            .Get<AuthSettings>();

        serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(authSetting.SecretKey)),
                    ClockSkew = TimeSpan.Zero
                };
                
                // Чтение токена только из куки
                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        // Получаем токен из куки
                        context.Token = context.Request.Cookies["jwt_token"];
                        return Task.CompletedTask;
                    }
                };
            });

        return serviceCollection;
    }

    // public static IServiceCollection AddAuth(this IServiceCollection serviceCollection, IConfiguration config)
    // {
    //     var authSetting = config.GetSection(nameof(AuthSettings))
    //         .Get<AuthSettings>();

    //     serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //         .AddJwtBearer(o =>
    //         {
    //             o.TokenValidationParameters = new TokenValidationParameters
    //             {
    //                 ValidateIssuer = false,
    //                 ValidateAudience = false,
    //                 ValidateLifetime = true,
    //                 ValidateIssuerSigningKey = true,
    //                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSetting.SecretKey))
    //             };
    //         });

    //     // opt

    //     // serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //     //     .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options
    //     //     (

    //     //     );

    //     return serviceCollection;
    // }

    public static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAccountRepository, PSQLAccountRepository>(); 

        serviceCollection.AddDbContext<AccountContext>( x =>
        {
            x.UseNpgsql("Host=account_db;Database=account_db;Username=postgres;Password=postgres;");    
        });

        return serviceCollection;
    }
}
