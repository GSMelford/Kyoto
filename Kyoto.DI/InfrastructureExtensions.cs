using Confluent.Kafka;
using Kyoto.Database;
using Kyoto.Database.BotClient;
using Kyoto.Database.BotClient.Repositories.Deploy;
using Kyoto.Database.BotFactory;
using Kyoto.Database.BotFactory.Repositories.Authorization;
using Kyoto.Database.BotFactory.Repositories.Deploy;
using Kyoto.Database.BotFactory.Repositories.Tenant;
using Kyoto.Domain.BotClient.Deploy.Interfaces;
using Kyoto.Domain.BotFactory.Authorization.Interfaces;
using Kyoto.Domain.RequestSystem;
using Kyoto.Domain.Tenant;
using Kyoto.Domain.Tenant.Interfaces;
using Kyoto.Kafka;
using Kyoto.Kafka.Interfaces;
using Kyoto.Services.BotClient.Deploy;
using Kyoto.Services.BotFactory.Authorization;
using Kyoto.Services.BotFactory.DeployStatus;
using Kyoto.Services.Deploy;
using Kyoto.Services.RequestSystem;
using Kyoto.Services.Tenant;
using Kyoto.Settings;
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
    
    public static IServiceCollection AddDatabaseBotFactory(this IServiceCollection services, DatabaseSettings databaseSettings, string tenantKey) 
    {
        return services.AddScoped<IDatabaseContext, DatabaseBotFactoryContext>(
            _ => new DatabaseBotFactoryContext(databaseSettings.ToConnectionString(tenantKey)));
    }
    
    public static IServiceCollection AddDatabaseBotClient(this IServiceCollection services, DatabaseSettings databaseSettings) 
    {
        return services.AddScoped<IDatabaseContext, DatabaseBotClientContext>(
            _ => new DatabaseBotClientContext(databaseSettings.ToConnectionString(CurrentBotTenant.BotTenant?.TenantKey)));
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

    public static IServiceCollection AddBotFactoryDeploy(this IServiceCollection services)
    {
        return services
            .AddTransient<IDeployService, DeployService>()
            .AddTransient<IDeployStatusService, DeployBotFactoryStatusService>()
            .AddTransient<IDeployRepository, DeployBotFactoryRepository>();
    }
    
    public static IServiceCollection AddBotClientDeploy(this IServiceCollection services)
    {
        return services
            .AddTransient<IDeployService, DeployService>()
            .AddTransient<IDeployStatusService, DeployBotClientStatusService>()
            .AddTransient<IDeployRepository, DeployBotClientRepository>();
    }
    
    public static IServiceCollection AddRequestService(this IServiceCollection services)
    {
        return services.AddHttpClient<IRequestService, RequestService>().Services;
    }
}