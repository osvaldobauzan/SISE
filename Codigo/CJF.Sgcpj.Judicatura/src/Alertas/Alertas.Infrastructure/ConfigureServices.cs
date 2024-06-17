using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddAlertasInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddLogging();
        return services;
    }





}
