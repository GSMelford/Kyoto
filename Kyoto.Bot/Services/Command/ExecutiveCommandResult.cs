using Kyoto.Domain.Command;

namespace Kyoto.Bot.Services.Command;

public class ExecutiveCommandResult
{
    public ExecutiveCommandStep? NextState { get; private set; }
    public string? AdditionalData { get; private set; }

    private ExecutiveCommandResult(ExecutiveCommandStep? nextState = null, string? additionalData = null)
    {
        NextState = nextState;
        AdditionalData = additionalData;
    }

    public static ExecutiveCommandResult CreateCompleted()
    {
        return new ExecutiveCommandResult();
    }

    public static ExecutiveCommandResult Create(ExecutiveCommandStep? nextState = null, string? additionalData = null)
    {
        return new ExecutiveCommandResult(nextState, additionalData);
    }
}