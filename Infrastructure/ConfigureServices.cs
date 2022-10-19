using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        if (configuration.GetValue<bool>("InDeveloping"))
        {
            services.AddDbContext<TeaShopBotDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
        else
        {
            services.AddDbContext<TeaShopBotDbContext>(options =>
            options.UseNpgsql(GetConnectionString()));
        }

        services.AddScoped<ITeaShopBotDbContext>(provider =>
        provider.GetRequiredService<TeaShopBotDbContext>());

        services.AddTransient<IDateTime, DateTimeService>();

        return services;
    }

    private static string GetConnectionString()
    {
        string? connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        string userPassSide = connectionUrl.Split("@")[0];
        string hostSide = connectionUrl.Split("@")[1];

        string user = userPassSide.Split(":")[1].Substring(2);
        string password = userPassSide.Split(':')[2];
        string host = hostSide.Split("/")[0];
        var database = hostSide.Split("/")[1].Split("?")[0];

        //Console.WriteLine($"Host={host};Database={database};Username={user};Password={password};SSL Mode=Require;Trust Server Certificate=true");
        return $"Host={host};Database={database};Username={user};Password={password};SSL Mode=Require;Trust Server Certificate=true";
    }
}
