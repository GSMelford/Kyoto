using Kyoto.Domain.CommandSystem;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.BotFactory;

public class BotFactoryCommandSet : BaseCommandSet
{
    protected override List<string> Commands { get; set; } = new ()
    {
        CommandCodes.Registration,
        CommandCodes.DeleteBot,
        CommandCodes.BotRegistration,
        CommandCodes.DeployBot,
        CommandCodes.DisableBot
    };
}