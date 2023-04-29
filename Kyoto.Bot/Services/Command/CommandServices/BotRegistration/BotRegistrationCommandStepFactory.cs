using Kyoto.Domain.Command;

namespace Kyoto.Bot.Services.Command.CommandServices.BotRegistration;

public class BotRegistrationCommandStepFactory : BaseCommandStepFactory
{
    public override CommandType CommandType => CommandType.BotRegistration;

    protected override List<Type> CommandStepTypes { get; set; } = new ()
    {
        typeof(BotRegistrationTokenStep),
        typeof(BotRegistrationConfirmNameStep)
    };
}