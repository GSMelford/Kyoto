using Kyoto.Domain.CommandSystem.Interfaces;

namespace Kyoto.Services.CommandSystem;

public abstract class BaseCommandSet : ICommandSet
{
    protected abstract List<string> Commands { get; set; }

    public bool IsExists(string commandName)
    {
        return Commands.Contains(commandName);
    }
}