using Kyoto.Domain.Tenant.Interfaces;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Kyoto.Settings;

namespace Kyoto.Services.Tenant;

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
            Token = _botTenantSettings.Token,
            IsFactory = true,
            IsAutomaticInitialization = true
        }, string.Empty);
    }
    
    public async Task InitBotTenantsFromDatabaseAsync()
    {
        await foreach (var botTenant in _tenantRepository.GetAllActiveTenantsAsync())
        {
            await _kafkaProducer.ProduceAsync(new InitTenantEvent
            {
                SessionId = Guid.NewGuid(),
                TenantKey = botTenant.TenantKey,
                Token = botTenant.Token,
                IsAutomaticInitialization = true
            }, string.Empty);
        }
    }
}