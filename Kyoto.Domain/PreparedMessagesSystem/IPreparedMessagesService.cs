using Kyoto.Domain.System;

namespace Kyoto.Domain.PreparedMessagesSystem;

public interface IPreparedMessagesService
{
    Task ProcessAsync(Session session, string messageText);
}