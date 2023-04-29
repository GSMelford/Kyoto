namespace Kyoto.Domain.Command;

public enum CommandType
{
    Registration,
    BotRegistration,
    DeployBot
}

public static class CommandTypeExtension
{
    public static string GetName(this CommandType commandType)
    {
        return commandType switch
        {
            CommandType.Registration => nameof(CommandType.Registration),
            CommandType.BotRegistration => nameof(CommandType.BotRegistration),
            CommandType.DeployBot => nameof(CommandType.DeployBot),
            _ => throw new ArgumentOutOfRangeException(nameof(commandType), commandType, null)
        };
    }
}