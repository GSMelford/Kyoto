using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.PostSystem;

public interface IResponseHandler
{
    Task ExecuteAsync(Session session, Message message);
}