using Kyoto.Bot.Core.ExecutiveCommandSystem;

namespace Kyoto.Bot.Commands.RegistrationCommand;

public class RegistrationCommandStepFactory : BaseCommandStepFactory
{
    public override string CommandName => "Registration";
    
    protected override List<Type> CommandStepTypes { get; set; } = new()
    {
        typeof(RegisterStep)
    };
}