using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.Processors;

public interface IMessageService
{
    Task ProcessAsync(Guid sessionId, Message message);
}