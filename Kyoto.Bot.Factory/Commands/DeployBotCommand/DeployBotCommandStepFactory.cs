using Kyoto.Bot.Core.ExecutiveCommandSystem;

namespace Kyoto.Bot.Commands.DeployBotCommand;

public class DeployBotCommandStepFactory : BaseCommandStepFactory
{
    public override string CommandName => "DeployBot";

    protected override List<Type> CommandStepTypes { get; set; } = new ()
    {
        typeof(SelectDeployBotCommandStep)
    };
}