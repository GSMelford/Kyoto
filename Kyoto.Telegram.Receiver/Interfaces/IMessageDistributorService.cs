using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Telegram.Receiver.Interfaces;

public interface IMessageDistributorService
{
    public Task DefineAsync(Guid sessionId, Message message);
}