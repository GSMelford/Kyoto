using Kyoto.Domain.System;
using Kyoto.Kafka.Modules;

namespace Kyoto.Kafka.Event;

public class BaseSessionEvent : BaseEvent
{
    public string TenantKey { get; set; } = null!;
    public long ChatId { get; set; }
    public long ExternalUserId { get; set; }
    public int MessageId { get; set; }

    public Session GetSession()
    {
        return Session.Create(SessionId, ChatId, ExternalUserId, MessageId, TenantKey);
    }

    public BaseSessionEvent()
    {
        
    }

    public BaseSessionEvent(Session session)
    {
        SessionId = session.Id;
        TenantKey = session.TenantKey;
        ChatId = session.ChatId;
        MessageId = session.MessageId;
        ExternalUserId = session.ExternalUserId;
    }
}