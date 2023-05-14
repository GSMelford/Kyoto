using Kyoto.Domain.BotClient.Deploy.Interfaces;
using Kyoto.Domain.Deploy;
using Kyoto.Domain.System;
using Kyoto.Domain.Tenant;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Kyoto.Services.Tenant;

namespace Kyoto.Bot.Client;

public class InitClientTenantHandler : IKafkaHandler<InitTenantEvent>
{
    private readonly IKafkaEventSubscriber _kafkaEventSubscriber;
    private readonly IDeployService _deployService;
    private readonly IKafkaProducer<string> _kafkaProducer;

    public InitClientTenantHandler(IKafkaEventSubscriber kafkaEventSubscriber, IDeployService deployService, IKafkaProducer<string> kafkaProducer)
    {
        _kafkaEventSubscriber = kafkaEventSubscriber;
        _deployService = deployService;
        _kafkaProducer = kafkaProducer;
    }

    public async Task HandleAsync(InitTenantEvent initTenantEvent)
    {
        var isNew = BotTenantFactory.Store.AddOrUpdateTenant(
            BotTenantModel.Create(
                initTenantEvent.TenantKey,
                initTenantEvent.Token));

        if (isNew && !initTenantEvent.IsFactory)
        {
            await _kafkaEventSubscriber.SubscribeAsync(initTenantEvent.TenantKey);
            await _deployService.DeployAsync(new InitTenantInfo
            {
                TenantKey = initTenantEvent.TenantKey,
                TemplateMessages = "TemplateMessagesClient.json"
            });

            if (!initTenantEvent.IsAutomaticInitialization)
            {
                await _kafkaProducer.ProduceAsync(new DeployStatusEvent(
                    Session.CreatePersonalNew(initTenantEvent.TenantKey, initTenantEvent.OwnerExternalUserId)), string.Empty);
            }
        }
    }
}