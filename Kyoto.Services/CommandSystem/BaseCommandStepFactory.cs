using Kyoto.Domain.CommandSystem.Interfaces;

namespace Kyoto.Services.CommandSystem;

public abstract class BaseCommandStepFactory : ICommandStepFactory
{
    protected abstract List<Type> CommandStepTypes { get; set; }
    public abstract string CommandName { get; }
    
    public virtual Type GetCommandStep(int step)
    {
        return CommandStepTypes[step];
    }

    public bool HasNext(int step)
    {
        return step + 1 < CommandStepTypes.Count;
    }
}