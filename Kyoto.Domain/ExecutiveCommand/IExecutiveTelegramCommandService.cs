using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.ExecutiveCommand;

public interface IExecutiveTelegramCommandService
{
    Task HandleExecutiveCommandIfExistAsync(Guid sessionId, Message message);
}