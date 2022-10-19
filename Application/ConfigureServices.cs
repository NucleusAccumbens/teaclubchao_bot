using Application.Herbs.Commands;
using Application.Herbs.Interfaces;
using Application.Herbs.Queries;
using Application.Honeys.Commands;
using Application.Honeys.Interfaces;
using Application.Honeys.Queries;
using Application.Orders.Commands;
using Application.Orders.Interfaces;
using Application.Products.Commands;
using Application.Products.Interfaces;
using Application.Products.Queries;
using Application.Teas.Commands;
using Application.Teas.Interfaces;
using Application.Teas.Queries;
using Application.TlgUsers.Commands;
using Application.TlgUsers.Interfaces;
using Application.TlgUsers.Queries;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICreateTlgUserCommand, CreateTlgUserCommand>();
        services.AddScoped<IСheckUserIsAdminQuery, СheckUserIsAdminQuery>();
        services.AddScoped<ICreateTeaCommand, CreateTeaCommand>();
        services.AddScoped<ICreateHerbCommand, CreateHerbCommand>();
        services.AddScoped<ICreateHoneyCommand, CreateHoneyCommand>();
        services.AddScoped<IKickTlgUserCommand, KickTlgUserCommand>();
        services.AddScoped<IGetUserLanguageQuery, GetUserLanguageQuery>();
        services.AddScoped<ICheckUserIsInDbQuery, CheckUserIsInDbQuery>();
        services.AddScoped<IChangeLanguageCommand, ChangeLanguageCommand>();
        services.AddScoped<IGetTeaQuery, GetTeasQuery>();
        services.AddScoped<IGetHerbQuery, GetHerbsQuery>();
        services.AddScoped<IGetHoneyQuery, GetHoneyQuery>();
        services.AddScoped<IChangeTeaCountCommand, ChangeTeaCountCommand>();
        services.AddScoped<IChangeHerbCountCommand, ChangeHerbCountCommand>();
        services.AddScoped<IChangeHoneyCountCommand, ChangeHoneyCountCommand>();
        services.AddScoped<ICreateOrderCommand, CreateOrderCommand>();
        services.AddScoped<IGetAdminsQuery, GetAdminsQuery>();
        services.AddScoped<IChangeProductCountCommand, ChangeProductCountCommand>();
        services.AddScoped<IUpdateTeaCommand, UpdateTeaCommand>();
        services.AddScoped<IDeleteProductCommand, DeleteProductCommand>();
        services.AddScoped<IUpdateHerbCommand, UpdateHerbCommand>();
        services.AddScoped<IUpdateHoneyCommand, UpdateHoneyCommand>();
        services.AddScoped<IGetProductsQuery, GetProductsQuery>();
        services.AddScoped<IUpdateProductCommand, UpdateProductCommand>();

        return services;
    }
}

