using Kyoto.Services.ExecuteCommand;

namespace Kyoto.Commands.BotFactory.BotRegistrationCommand;

public class BotRegistrationCommandStepFactory : BaseCommandStepFactory
{
    public override string CommandName => "RegisterNewBot";

    protected override List<Type> CommandStepTypes { get; set; } = new ()
    {
        typeof(BotRegistrationTokenStep),
        typeof(BotRegistrationConfirmNameStep)
    };
}