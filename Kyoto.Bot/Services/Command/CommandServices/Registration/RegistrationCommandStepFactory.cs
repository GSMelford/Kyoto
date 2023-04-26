using Kyoto.Domain.Command;

namespace Kyoto.Bot.Services.Command.CommandServices.Registration;

public class RegistrationCommandStepFactory : BaseCommandStepFactory
{
    public override CommandType CommandType => CommandType.Registration;

    protected override List<Type> CommandStepTypes { get; set; } = new()
    {
        typeof(RegisterStep)
    };
}