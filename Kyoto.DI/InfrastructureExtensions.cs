using Confluent.Kafka;
using Kyoto.Dal;
using Kyoto.Dal.BotFactory;
using Kyoto.Dal.BotFactory.Repositories.Authorization;
using Kyoto.Dal.BotFactory.Repositories.Tenant;
using Kyoto.Domain.BotFactory.Authorization.Interfaces;
using Kyoto.Domain.Settings;
using Kyoto.Domain.Tenant.Interfaces;
using Kyoto.Kafka;
using Kyoto.Kafka.Interfaces;
using Kyoto.Services.BotFactory.Authorization;
using Kyoto.Services.Tenant;
using Microsoft.Extensions.DependencyInjection;

namespace Kyoto.DI;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IAuthorizationRepository, AuthorizationRepository>()
            .AddTransient<IAuthorizationService, AuthorizationService>();
    }
    
    public static IServiceCollection AddDatabase(this IServiceCollection services, DatabaseSettings databaseSettings) 
    {
        return services.AddScoped<IDatabaseContext, DatabaseBotFactoryContext>(
            _ => new DatabaseBotFactoryContext(databaseSettings.ToConnectionString()));
    }
    
    public static IServiceCollection AddKafka(this IServiceCollection services, KafkaSettings kafkaSettings)
    {
        services.AddKafkaProducer<string>(new ProducerConfig { BootstrapServers = kafkaSettings.BootstrapServers });
        services.AddKafkaConsumersFactory();

        return services;
    }

    public static IServiceCollection AddTenant(this IServiceCollection services, BotTenantSettings botTenantSettings)
    {
        return services
            .AddTransient<ITenantService, TenantService>(provider =>
            {
                var kafkaProducer = provider.GetRequiredService<IKafkaProducer<string>>();
                var tenantRepository = provider.GetRequiredService<ITenantRepository>();
                return new TenantService(kafkaProducer, botTenantSettings, tenantRepository);
            })
            .AddTransient<ITenantRepository, TenantRepository>();
    }
}