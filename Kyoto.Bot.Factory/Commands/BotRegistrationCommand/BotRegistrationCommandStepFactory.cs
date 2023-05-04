using Kyoto.Bot.Core.ExecutiveCommandSystem;

namespace Kyoto.Bot.Commands.BotRegistrationCommand;

public class BotRegistrationCommandStepFactory : BaseCommandStepFactory
{
    public override string CommandName => "BotRegistration";

    protected override List<Type> CommandStepTypes { get; set; } = new ()
    {
        typeof(BotRegistrationTokenStep),
        typeof(BotRegistrationConfirmNameStep)
    };
}