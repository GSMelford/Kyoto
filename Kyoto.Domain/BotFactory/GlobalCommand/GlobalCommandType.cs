namespace Kyoto.Domain.BotFactory.GlobalCommand;

public enum GlobalCommandType
{
    Start,
    NotFound
}

public static class GlobalCommandTypeExtension
{
    public static GlobalCommandType Get(this string value)
    {
        return value switch
        {
            "/start" => GlobalCommandType.Start,
            _ => GlobalCommandType.NotFound
        };
    }
}