using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelMore.Persistance.Contexts.TravelMore;

namespace TravelMore.Persistance;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        return services
            .AddDatabase(configuration);
    }


    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        return services.AddDbContext<TravelMoreContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(TravelMoreContext.SectionName));
        });
    }
}
