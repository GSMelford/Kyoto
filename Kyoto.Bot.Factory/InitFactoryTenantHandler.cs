using Kyoto.Domain.BotClient.Deploy.Interfaces;
using Kyoto.Domain.Deploy;
using Kyoto.Domain.Tenant;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Kyoto.Services.Tenant;

namespace Kyoto.Bot.Factory;

public class InitFactoryTenantHandler : IKafkaHandler<InitTenantEvent>
{
    private readonly IKafkaEventSubscriber _kafkaEventSubscriber;
    private readonly IDeployService _deployService;

    public InitFactoryTenantHandler(IKafkaEventSubscriber kafkaEventSubscriber, IDeployService deployService)
    {
        _kafkaEventSubscriber = kafkaEventSubscriber;
        _deployService = deployService;
    }

    public async Task HandleAsync(InitTenantEvent initTenantEvent)
    {
        var isNew = BotTenantFactory.Store.AddOrUpdateTenant(
            BotTenantModel.Create(
                initTenantEvent.TenantKey,
                initTenantEvent.Token));

        if (isNew && initTenantEvent.IsFactory)
        {
            await _kafkaEventSubscriber.SubscribeAsync(initTenantEvent.TenantKey);
            await _deployService.DeployAsync(new InitTenantInfo
            {
                TenantKey = initTenantEvent.TenantKey,
                TemplateMessages = "TemplateMessagesFactory.json"
            });
        }
    }
}