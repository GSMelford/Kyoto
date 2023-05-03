using Kyoto.Bot.Commands.ExecutiveCommandSystem;
using Kyoto.Domain.Command;

namespace Kyoto.Bot.Commands.DeployBotCommand;

public class DeployBotCommandStepFactory : BaseCommandStepFactory
{
    public override CommandType CommandType => CommandType.DeployBot;

    protected override List<Type> CommandStepTypes { get; set; } = new ()
    {
        typeof(SelectDeployBotCommandStep)
    };
}