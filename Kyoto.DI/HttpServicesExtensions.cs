using Kyoto.Services.HttpServices.BotRegistration;
using Microsoft.Extensions.DependencyInjection;

namespace Kyoto.DI;

public static class HttpServicesExtensions
{
    public static IServiceCollection AddHttpServices(this IServiceCollection services)
    {
        return services.AddHttpClient<BotRegistrationHttpService>().Services;
    }
}