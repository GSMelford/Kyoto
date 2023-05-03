using Kyoto.Domain.Command;

namespace Kyoto.Bot.Commands.ExecutiveCommandSystem;

public abstract class BaseCommandStepFactory : ICommandStepFactory
{
    protected abstract List<Type> CommandStepTypes { get; set; }
    public abstract CommandType CommandType { get; }
    
    public virtual Type GetCommandStep(ExecutiveCommandStep commandStep)
    {
        var index = (int)commandStep;
        return CommandStepTypes[index];
    }

    public bool HasNext(ExecutiveCommandStep commandStep)
    {
        var index = (int)commandStep;
        return index + 1 < CommandStepTypes.Count;
    }
}