using Kyoto.Domain.CommandSystem;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.BotFactory.DisableBotCommand;

public class DisableBotCommandStepFactory : BaseCommandStepFactory
{
    public override string CommandName => CommandCodes.BotManagement.DisableBot;

    protected override List<Type> CommandStepTypes { get; set; } = new()
    {
        typeof(DisableBotCommandStep)
    };
}