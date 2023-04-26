namespace Kyoto.Domain.Command;

public enum CommandType
{
    Registration,
    BotRegistration
}

public static class CommandTypeExtension
{
    public static string GetName(this CommandType commandType)
    {
        return commandType switch
        {
            CommandType.Registration => nameof(CommandType.Registration),
            CommandType.BotRegistration => nameof(CommandType.BotRegistration),
            _ => throw new ArgumentOutOfRangeException(nameof(commandType), commandType, null)
        };
    }
}