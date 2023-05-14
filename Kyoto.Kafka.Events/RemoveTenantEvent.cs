using Kyoto.Kafka.Modules;

namespace Kyoto.Kafka.Event;

public class RemoveTenantEvent : BaseEvent
{
    public string TenantKey { get; set; } = null!;
}