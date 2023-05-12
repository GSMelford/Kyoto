using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.BotFactory.RegistrationCommand;

public class RegistrationCommandStepFactory : BaseCommandStepFactory
{
    public override string CommandName => "Registration";
    
    protected override List<Type> CommandStepTypes { get; set; } = new()
    {
        typeof(RegisterStep)
    };
}