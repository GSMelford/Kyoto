using Kyoto.Domain.ExecutiveCommand;
using Kyoto.Domain.ExecutiveCommand.Interfaces;

namespace Kyoto.Services.ExecuteCommand;

public abstract class BaseCommandStepFactory : ICommandStepFactory
{
    protected abstract List<Type> CommandStepTypes { get; set; }
    public abstract string CommandName { get; }
    
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