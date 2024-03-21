using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelMore.Application.Common.Interfaces.Repositories;
using TravelMore.Application.Common.Interfaces.Services;
using TravelMore.Infrastructure.Middlewares;
using TravelMore.Infrastructure.Repositories;
using TravelMore.Infrastructure.Services;

namespace TravelMore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        return services
            .AddRepositories();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddTransient<GlobalExceptionLoggingMiddleware>()
            .AddScoped<IUserIdentityService, UserIdentityService>()
            .AddScoped<IBookingRepository, BookingRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IHostRepository, HostRepository>()
            .AddScoped<IGuestRepository, GuestRepository>()
            .AddScoped<IHotelRepository, HotelRepository>();

    }
}
