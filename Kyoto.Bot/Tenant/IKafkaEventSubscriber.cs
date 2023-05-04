namespace Kyoto.Bot.Core.Tenant;

public interface IKafkaEventSubscriber
{
    public Task SubscribeAsync(string tenantKey, string bootstrapServers);
}