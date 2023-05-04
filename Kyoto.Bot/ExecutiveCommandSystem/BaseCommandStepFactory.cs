using ExecutiveCommandStep = Kyoto.Bot.Core.ExecutiveCommandSystem.Models.ExecutiveCommandStep;

namespace Kyoto.Bot.Core.ExecutiveCommandSystem;

public abstract class BaseCommandStepFactory : Interfaces.ICommandStepFactory
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