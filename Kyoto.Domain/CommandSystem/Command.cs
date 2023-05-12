namespace Kyoto.Domain.CommandSystem;

public class Command
{
    public Guid SessionId { get; private set; }
    public long ChatId { get; private set; }
    public long ExternalUserId { get; private set; }
    public string Name { get; private set; }
    public int Step { get; private set; }
    public CommandState State { get; private set; }
    public string? AdditionalData { get; private set; }

    private Command(
        Guid sessionId, 
        long chatId, 
        long externalUserId, 
        string name, 
        string? additionalData, 
        int step, 
        CommandState state)
    {
        SessionId = sessionId;
        ChatId = chatId;
        ExternalUserId = externalUserId;
        Name = name;
        AdditionalData = additionalData;
        Step = step;
        State = state;
    }

    public static Command Create(
        Guid sessionId,
        long chatId, 
        long externalUserId,
        string commandValue,
        string? additionalData,
        int step,
        CommandState state)
    {
        return new Command(sessionId, chatId, externalUserId, commandValue, additionalData, step, state);
    }

    public void SetState(CommandState commandState)
    {
        State = commandState;
    }
    
    public void ResetState()
    {
        State = CommandState.RequestToAction;
    }
    
    public void IncreaseStep()
    {
        Step++;
    }
    
    public void SetStep(int step)
    {
        Step = step;
    }
    
    public void SetAdditionalData(string? additionalData)
    {
        AdditionalData = additionalData;
    }
}