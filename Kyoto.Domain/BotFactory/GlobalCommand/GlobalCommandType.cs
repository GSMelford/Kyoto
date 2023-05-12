namespace Kyoto.Domain.BotFactory.GlobalCommand;

public enum GlobalCommandType
{
    Registration,
    NotFound
}

public static class GlobalCommandTypeExtension
{
    public static GlobalCommandType Get(this string value)
    {
        return value switch
        {
            "/start" => GlobalCommandType.Registration,
            _ => GlobalCommandType.NotFound
        };
    }
}