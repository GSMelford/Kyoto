using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.BotClient;

public class BotClientCommandSet : BaseCommandSet
{
    protected override List<string> Commands { get; set; } = new ();
}