using Kyoto.Domain.BotFactory.Menu;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.BotFactory.DeployBotCommand;

public class DeployBotCommandStepFactory : BaseCommandStepFactory
{
    public override string CommandName => MenuButtons.BotManagementButtons.DeployBot;

    protected override List<Type> CommandStepTypes { get; set; } = new ()
    {
        typeof(SelectDeployBotCommandStep)
    };
}