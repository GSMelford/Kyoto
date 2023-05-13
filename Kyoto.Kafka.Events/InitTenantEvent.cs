using Kyoto.Kafka.Modules;

namespace Kyoto.Kafka.Event;

public class InitTenantEvent : BaseEvent
{
    public bool IsFactory { get; set; }
    public bool NewlyCreated { get; set; }
    public string TenantKey { get; set; } = null!;
    public long OwnerExternalUserId { get; set; }
    public string Token { get; set; } = null!;
}