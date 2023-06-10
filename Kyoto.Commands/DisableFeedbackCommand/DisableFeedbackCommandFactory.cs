using Kyoto.Commands.CommonSteps;
using Kyoto.Domain.CommandSystem;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.DisableFeedbackCommand;

public class DisableFeedbackCommandFactory : BaseCommandStepFactory
{
    public override string CommandName => CommandCodes.BotFeatures.DisableCollectFeedback;

    protected override List<Type> CommandStepTypes { get; set; } = new()
    {
        typeof(SelectBotCommandStep),
        typeof(DisableFeedbackCommandStep)
    };
}