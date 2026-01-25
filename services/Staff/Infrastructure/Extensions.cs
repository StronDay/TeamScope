using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Namotion.Reflection;
using Staff.Core.App.Abstractions;
using Staff.Core.App.Filters;
using Staff.Core.App.Services;
using Staff.Core.Domain.Models;
using Staff.Infrastructure.Repositories;

namespace Staff.Infrastructure;

public static class Extensions
{
    public static IQueryable<StaffModel> Filter(this IQueryable<StaffModel> query, StaffFilter staffFilter)
    {
        if (!string.IsNullOrEmpty(staffFilter.JobPosition))
            query = query.Where(x  => x.JobPosition == staffFilter.JobPosition);
            
        if (staffFilter.Age.HasValue && staffFilter.Age.Value > 0)
            query = query.Where(x  => x.Age == staffFilter.Age);

        return query;
    }

    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IStaffService, StaffService>();

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

    public static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IStaffRepository, PSQLStaffRepository>(); 

        serviceCollection.AddDbContext<StaffContext>( x =>
        {
            x.UseNpgsql("Host=staff_db;Database=staff_db;Username=postgres;Password=postgres;");    
        });

        return serviceCollection;
    }
}
