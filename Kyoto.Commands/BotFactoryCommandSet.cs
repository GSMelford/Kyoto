using Kyoto.Domain.CommandSystem;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands;

public class BotFactoryCommandSet : BaseCommandSet
{
    protected override List<string> Commands { get; set; } = new ()
    {
        CommandCodes.Registration,
        CommandCodes.BotManagement.DeleteBot,
        CommandCodes.BotManagement.BotRegistration,
        CommandCodes.BotManagement.DeployBot,
        CommandCodes.BotManagement.DisableBot,
        CommandCodes.BotFeatures.SetRegistration,
        CommandCodes.BotFeatures.AddNewsletter
    };
}