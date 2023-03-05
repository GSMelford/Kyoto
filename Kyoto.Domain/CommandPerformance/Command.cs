namespace Kyoto.Domain.CommandPerformance;

public enum Command
{
    Start,
    NotFound
}

public static class CommandExtension
{
    public static Command Get(string value)
    {
        return value switch
        {
            "/start" => Command.Start,
            _ => Command.NotFound
        };
    }
}