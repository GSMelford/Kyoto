﻿namespace Kyoto.Kafka.Events;

public abstract class BaseEvent
{
    public Guid SessionId { get; set; }
}