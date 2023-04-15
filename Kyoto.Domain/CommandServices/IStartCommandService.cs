namespace Kyoto.Domain.CommandServices;

public interface IStartCommandService
{
    Task ExecuteAsync(Guid sessionId, long chatId, long telegramId);
}