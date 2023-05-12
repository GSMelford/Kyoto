using Kyoto.Domain.BotFactory.Menu;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.BotFactory;

public class BotFactoryCommandSet : BaseCommandSet
{
    protected override List<string> Commands { get; set; } = new ()
    {
        "Registration",
        MenuButtons.BotManagementButtons.DeployBot,
        MenuButtons.BotManagementButtons.DeleteBot,
        MenuButtons.BotManagementButtons.DisableBot,
        MenuButtons.BotManagementButtons.RegisterNewBot
    };
}