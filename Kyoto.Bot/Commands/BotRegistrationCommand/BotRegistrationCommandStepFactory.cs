using Kyoto.Bot.Commands.ExecutiveCommandSystem;
using Kyoto.Domain.Command;

namespace Kyoto.Bot.Commands.BotRegistrationCommand;

public class BotRegistrationCommandStepFactory : BaseCommandStepFactory
{
    public override CommandType CommandType => CommandType.BotRegistration;

    protected override List<Type> CommandStepTypes { get; set; } = new ()
    {
        typeof(BotRegistrationTokenStep),
        typeof(BotRegistrationConfirmNameStep)
    };
}