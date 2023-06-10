using Kyoto.Domain.CommandSystem;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.DeployBotCommand;

public class DeployBotCommandStepFactory : BaseCommandStepFactory
{
    public override string CommandName => CommandCodes.BotManagement.DeployBot;

    protected override List<Type> CommandStepTypes { get; set; } = new ()
    {
        typeof(DeployBotCommandStep)
    };
}