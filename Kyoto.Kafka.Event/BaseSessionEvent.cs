using Kyoto.Domain.System;
using Kyoto.Kafka.Modules;

namespace Kyoto.Kafka.Event;

public class BaseSessionEvent : BaseEvent
{
    public long ChatId { get; set; }
    public long ExternalUserId { get; set; }
    public int MessageId { get; set; }

    public Session GetSession()
    {
        return Session.Create(SessionId, ChatId, ExternalUserId, MessageId);
    } 
}