using Kyoto.Bot.Commands.ExecutiveCommandSystem;
using Kyoto.Domain.Command;

namespace Kyoto.Bot.Commands.RegistrationCommand;

public class RegistrationCommandStepFactory : BaseCommandStepFactory
{
    public override CommandType CommandType => CommandType.Registration;

    protected override List<Type> CommandStepTypes { get; set; } = new()
    {
        typeof(RegisterStep)
    };
}