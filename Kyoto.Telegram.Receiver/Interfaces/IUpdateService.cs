using Kyoto.Domain.Telegram.Updates;

namespace Kyoto.Telegram.Receiver.Interfaces;

public interface IUpdateService
{
    Task HandleAsync(Update update);
}