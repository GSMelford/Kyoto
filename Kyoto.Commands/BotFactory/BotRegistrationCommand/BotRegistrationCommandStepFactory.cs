using Kyoto.Domain.CommandSystem;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.BotFactory.BotRegistrationCommand;

public class BotRegistrationCommandStepFactory : BaseCommandStepFactory
{
    public override string CommandName => CommandCodes.BotManagement.BotRegistration;

    protected override List<Type> CommandStepTypes { get; set; } = new ()
    {
        typeof(BotRegistrationTokenStep),
        typeof(BotRegistrationConfirmNameStep)
    };
}