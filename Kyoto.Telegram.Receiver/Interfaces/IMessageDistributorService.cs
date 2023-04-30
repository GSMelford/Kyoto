using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Telegram.Receiver.Interfaces;

public interface IMessageDistributorService
{
    public Task DefineAsync(string tenantKey, Message message);
}