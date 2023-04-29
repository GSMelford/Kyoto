using Kyoto.Domain.Tenant;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Bot.KafkaHandlers;

public class RequestTenantHandler : IKafkaHandler<RequestTenantEvent>
{
    private readonly IKafkaProducer<string> _kafkaProducer;
    private readonly ITenantRepository _tenantRepository;

    public RequestTenantHandler(IKafkaProducer<string> kafkaProducer, ITenantRepository tenantRepository)
    {
        _kafkaProducer = kafkaProducer;
        _tenantRepository = tenantRepository;
    }

    public async Task HandleAsync(RequestTenantEvent @event)
    {
        await foreach (var botTenant in _tenantRepository.GetAllTenantsAsync())
        {
            await _kafkaProducer.ProduceAsync(new InitTenantEvent
            {
                TenantKey = botTenant.TenantKey,
                Token = botTenant.Token
            });
        }
    }
}