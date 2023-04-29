using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.PostSystem;

public interface IResponseMessageExecutor
{
    Task ExecuteAsync(Session session, Message message, string type);
}