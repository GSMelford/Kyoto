using Kyoto.Commands.CommonSteps;
using Kyoto.Domain.CommandSystem;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.EnableFeedbackCommand;

public class EnableFeedbackCommandFactory : BaseCommandStepFactory
{
    public override string CommandName => CommandCodes.BotFeatures.EnableCollectFeedback;

    protected override List<Type> CommandStepTypes { get; set; } = new()
    {
        typeof(SelectBotCommandStep),
        typeof(EnableFeedbackCommandStep)
    };
}