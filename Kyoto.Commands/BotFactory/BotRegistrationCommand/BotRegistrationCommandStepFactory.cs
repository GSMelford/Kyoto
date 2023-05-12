using Kyoto.Domain.BotFactory.Menu;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.BotFactory.BotRegistrationCommand;

public class BotRegistrationCommandStepFactory : BaseCommandStepFactory
{
    public override string CommandName => MenuButtons.BotManagementButtons.RegisterNewBot;

    protected override List<Type> CommandStepTypes { get; set; } = new ()
    {
        typeof(BotRegistrationTokenStep),
        typeof(BotRegistrationConfirmNameStep)
    };
}