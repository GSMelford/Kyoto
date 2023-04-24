namespace Kyoto.Domain.Command;

public class ExecutiveCommand
{
    public Guid SessionId { get; private set; }
    public long ChatId { get; private set; }
    public long ExternalUserId { get; private set; }
    public ExecutiveCommandType ExecutiveCommandValue { get; private set; }
    public object? AdditionalData { get; set; }

    private ExecutiveCommand(
        Guid sessionId, 
        long chatId, 
        long externalUserId, 
        ExecutiveCommandType executiveCommandValue, 
        object? additionalData)
    {
        SessionId = sessionId;
        ChatId = chatId;
        ExternalUserId = externalUserId;
        ExecutiveCommandValue = executiveCommandValue;
        AdditionalData = additionalData;
    }

    public static ExecutiveCommand Create(
        Guid sessionId,
        long chatId, 
        long externalUserId,
        ExecutiveCommandType executiveCommandValue,
        object? additionalData)
    {
        return new ExecutiveCommand(sessionId, chatId, externalUserId, executiveCommandValue, additionalData);
    }
}