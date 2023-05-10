using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.PostSystem.Interfaces;

public interface IResponseMessageExecutor
{
    Task ExecuteAsync(Session session, Message message, string type);
}