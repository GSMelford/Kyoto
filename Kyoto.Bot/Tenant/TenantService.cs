using Kyoto.Domain.Tenant;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Bot.Core.Tenant;

public class TenantService : ITenantService
{
    private readonly IKafkaProducer<string> _kafkaProducer;
    private readonly BotTenantSettings _botTenantSettings;
    private readonly ITenantRepository _tenantRepository;
    
    public TenantService(IKafkaProducer<string> kafkaProducer, BotTenantSettings botTenantSettings, ITenantRepository tenantRepository)
    {
        _kafkaProducer = kafkaProducer;
        _botTenantSettings = botTenantSettings;
        _tenantRepository = tenantRepository;
    }

    public Task InitMainBotTenantAsync()
    {
        return _kafkaProducer.ProduceAsync(new InitTenantEvent
        {
            SessionId = Guid.NewGuid(),
            TenantKey = _botTenantSettings.Key,
            Token = _botTenantSettings.Token
        }, string.Empty);
    }
    
    public async Task InitBotTenantsFromDatabaseAsync()
    {
        await foreach (var botTenant in _tenantRepository.GetAllTenantsAsync())
        {
            await _kafkaProducer.ProduceAsync(new InitTenantEvent
            {
                SessionId = Guid.NewGuid(),
                TenantKey = botTenant.TenantKey,
                Token = botTenant.Token
            }, string.Empty);
        }
    }
}