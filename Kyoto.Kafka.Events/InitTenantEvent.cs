using Kyoto.Kafka.Modules;

namespace Kyoto.Kafka.Event;

public class InitTenantEvent : BaseEvent
{
    public string TenantKey { get; set; } = null!;
    public string Token { get; set; } = null!;
}