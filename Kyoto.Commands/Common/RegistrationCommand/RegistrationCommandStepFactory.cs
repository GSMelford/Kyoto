using Kyoto.Domain.CommandSystem;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.Common.RegistrationCommand;

public class RegistrationCommandStepFactory : BaseCommandStepFactory
{
    public override string CommandName => CommandCodes.Registration;
    
    protected override List<Type> CommandStepTypes { get; set; } = new()
    {
        typeof(RegisterStep)
    };
}