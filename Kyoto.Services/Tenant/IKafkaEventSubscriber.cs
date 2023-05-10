namespace Kyoto.Services.Tenant;

public interface IKafkaEventSubscriber
{
    public Task SubscribeAsync(string tenantKey);
}