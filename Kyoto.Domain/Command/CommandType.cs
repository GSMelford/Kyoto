namespace Kyoto.Domain.Command;

public enum CommandType
{
    Start,
    NotFound
}

public static class CommandTypeExtension
{
    public static CommandType Get(string value)
    {
        return value switch
        {
            "/start" => CommandType.Start,
            _ => CommandType.NotFound
        };
    }
}