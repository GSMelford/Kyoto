using Kyoto.Bot.StartUp.Settings;
using Kyoto.Domain.Tenant;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Bot.Services.Tenant;

public class TenantService : ITenantService
{
    private readonly IKafkaProducer<string> _kafkaProducer;
    private readonly AppSettings _appSettings;
    private readonly ITenantRepository _tenantRepository;
    
    public TenantService(IKafkaProducer<string> kafkaProducer, AppSettings appSettings, ITenantRepository tenantRepository)
    {
        _kafkaProducer = kafkaProducer;
        _appSettings = appSettings;
        _tenantRepository = tenantRepository;
    }

    public Task InitMainBotTenantAsync()
    {
        return _kafkaProducer.ProduceAsync(new InitTenantEvent
        {
            SessionId = Guid.NewGuid(),
            TenantKey = _appSettings.BotTenantSettings.Key,
            Token = _appSettings.BotTenantSettings.Token
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