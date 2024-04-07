using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelMore.Application.Repositories;
using TravelMore.Application.Services;
using TravelMore.Application.Services.PasswordHasher;
using TravelMore.Infrastructure.Middlewares;
using TravelMore.Infrastructure.Repositories;
using TravelMore.Infrastructure.Services;
using TravelMore.Infrastructure.Services.JwtTokenGenerators;

namespace TravelMore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddTransient<GlobalExceptionLoggingMiddleware>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();

        return services
            .AddRepositories()
            .AddCustomOptions(configuration);
    }

    private static IServiceCollection AddCustomOptions(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IUserIdentityService, UserIdentityService>()
            .AddScoped<IBookingRepository, BookingRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IHostRepository, HostRepository>()
            .AddScoped<IGuestRepository, GuestRepository>()
            .AddScoped<IStandardGuestRepository, StandardGuestRepository>()
            .AddScoped<IHotelRepository, HotelRepository>();

    }


}
