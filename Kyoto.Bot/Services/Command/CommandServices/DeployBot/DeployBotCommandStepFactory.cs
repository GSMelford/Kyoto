using Kyoto.Domain.Command;

namespace Kyoto.Bot.Services.Command.CommandServices.DeployBot;

public class DeployBotCommandStepFactory : BaseCommandStepFactory
{
    public override CommandType CommandType => CommandType.DeployBot;

    protected override List<Type> CommandStepTypes { get; set; } = new ()
    {
        typeof(SelectDeployBotCommandStep)
    };
}