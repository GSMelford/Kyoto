namespace Kyoto.Domain.CommandSystem;

public class CommandStepResult
{
    public bool IsRetry { get; private set; }
    public bool IsInterrupt { get; private set; }
    public int? ToRetryStep { get; private set; }

    public CommandStepResult(bool isRetry, bool isInterrupt, int? toRetryStep)
    {
        IsRetry = isRetry;
        IsInterrupt = isInterrupt;
        ToRetryStep = toRetryStep;
    }

    public static CommandStepResult CreateSuccessful()
    {
        return new CommandStepResult(false, false, default);
    }
    
    public static CommandStepResult CreateRetry(int? toStep = null)
    {
        return new CommandStepResult(true, false, toStep);
    }
    
    public static CommandStepResult CreateInterrupt()
    {
        return new CommandStepResult(false, true, default);
    }
}