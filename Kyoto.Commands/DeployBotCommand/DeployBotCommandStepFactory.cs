using Kyoto.Services.ExecuteCommand;

namespace Kyoto.Commands.DeployBotCommand;

public class DeployBotCommandStepFactory : BaseCommandStepFactory
{
    public override string CommandName => "DeployBot";

    protected override List<Type> CommandStepTypes { get; set; } = new ()
    {
        typeof(SelectDeployBotCommandStep)
    };
}