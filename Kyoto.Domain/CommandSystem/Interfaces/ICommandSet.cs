namespace Kyoto.Domain.CommandSystem.Interfaces;

public interface ICommandSet
{
    public bool IsExists(string commandName);
}