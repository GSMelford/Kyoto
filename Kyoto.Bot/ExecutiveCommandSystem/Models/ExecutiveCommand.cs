using Kyoto.Domain.Command;

namespace Kyoto.Bot.Core.ExecutiveCommandSystem.Models;

public class ExecutiveCommand
{
    public Guid SessionId { get; private set; }
    public long ChatId { get; private set; }
    public long ExternalUserId { get; private set; }
    public string CommandName { get; private set; }
    public ExecutiveCommandStep Step { get; private set; }
    public CommandStepState StepState { get; private set; }
    public string? AdditionalData { get; private set; }

    private ExecutiveCommand(
        Guid sessionId, 
        long chatId, 
        long externalUserId, 
        string commandName, 
        string? additionalData, 
        ExecutiveCommandStep step, 
        CommandStepState stepState)
    {
        SessionId = sessionId;
        ChatId = chatId;
        ExternalUserId = externalUserId;
        CommandName = commandName;
        AdditionalData = additionalData;
        Step = step;
        StepState = stepState;
    }

    public static ExecutiveCommand Create(
        Guid sessionId,
        long chatId, 
        long externalUserId,
        string commandValue,
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
    
    public void SetStep(ExecutiveCommandStep step)
    {
        Step = step;
    }
    
    public void SetAdditionalData(string? additionalData)
    {
        AdditionalData = additionalData;
    }
}