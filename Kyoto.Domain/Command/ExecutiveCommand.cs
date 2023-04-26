namespace Kyoto.Domain.Command;

public class ExecutiveCommand
{
    public Guid SessionId { get; private set; }
    public long ChatId { get; private set; }
    public long ExternalUserId { get; private set; }
    public CommandType CommandValue { get; private set; }
    public ExecutiveCommandStep Step { get; private set; }
    public CommandStepState StepState { get; private set; }
    public string? AdditionalData { get; set; }

    private ExecutiveCommand(
        Guid sessionId, 
        long chatId, 
        long externalUserId, 
        CommandType commandValue, 
        string? additionalData, 
        ExecutiveCommandStep step, 
        CommandStepState stepState)
    {
        SessionId = sessionId;
        ChatId = chatId;
        ExternalUserId = externalUserId;
        CommandValue = commandValue;
        AdditionalData = additionalData;
        Step = step;
        StepState = stepState;
    }

    public static ExecutiveCommand Create(
        Guid sessionId,
        long chatId, 
        long externalUserId,
        CommandType commandValue,
        string? additionalData,
        ExecutiveCommandStep step,
        CommandStepState stepState)
    {
        return new ExecutiveCommand(sessionId, chatId, externalUserId, commandValue, additionalData, step, stepState);
    }

    public void SetStepState(CommandStepState commandStepState)
    {
        StepState = commandStepState;
    }
    
    public void ResetState()
    {
        StepState = CommandStepState.RequestToAction;
    }
    
    public void IncreaseStep()
    {
        var temp = (int)Step;
        Step = (ExecutiveCommandStep)(++temp);
    }
}