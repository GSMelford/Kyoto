using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.Processors;

public interface IMessageService
{
    Task ProcessAsync(Session session, Message message);
}