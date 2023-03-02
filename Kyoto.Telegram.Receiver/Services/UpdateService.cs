using Kyoto.Domain.Telegram.Updates;
using Kyoto.Telegram.Receiver.Interfaces;

namespace Kyoto.Telegram.Receiver.Services;

public class UpdateService : IUpdateService
{
    public Task HandleAsync(Update update)
    {
        return Task.CompletedTask;
    }
}