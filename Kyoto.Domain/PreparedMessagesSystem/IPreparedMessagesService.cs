using Kyoto.Domain.System;

namespace Kyoto.Domain.PreparedMessagesSystem;

public interface IPreparedMessagesService
{
    Task<bool> ProcessAsync(Session session, string messageText);
}