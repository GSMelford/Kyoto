using Kyoto.Commands.CommonSteps;
using Kyoto.Domain.CommandSystem;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.GetFeedbackListCommand;

public class GetFeedbackListCommandStepFactory : BaseCommandStepFactory
{
    public override string CommandName => CommandCodes.BotFeatures.ShowFeedbacks;

    protected override List<Type> CommandStepTypes { get; set; } = new()
    {
        typeof(SelectBotCommandStep),
        typeof(GetFeedbackListCommandStep)
    };
}