namespace Kyoto.Domain.Command.GlobalCommand;

public enum GlobalCommandType
{
    Start,
    NotFound
}

public static class GlobalCommandTypeExtension
{
    public static GlobalCommandType Get(string value)
    {
        return value switch
        {
            "/start" => GlobalCommandType.Start,
            _ => GlobalCommandType.NotFound
        };
    }
}