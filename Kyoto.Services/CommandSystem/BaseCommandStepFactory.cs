using Kyoto.Domain.CommandSystem.Interfaces;

namespace Kyoto.Services.CommandSystem;

public abstract class BaseCommandStepFactory : ICommandStepFactory
{
    public abstract string CommandName { get; }
    protected abstract List<Type> CommandStepTypes { get; set; }
    
    public virtual Type GetCommandStep(int step)
    {
        return CommandStepTypes[step];
    }

    public bool HasNext(int step)
    {
        return step + 1 < CommandStepTypes.Count;
    }
}