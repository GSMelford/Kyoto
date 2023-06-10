using Kyoto.Domain.CommandSystem;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.SendFeedbackCommand;

public class SendFeedbackCommandStepFactory : BaseCommandStepFactory
{
    public override string CommandName => CommandCodes.Client.SendFeedback;

    protected override List<Type> CommandStepTypes { get; set; } = new()
    {
        typeof(SendFeedbackCommandStep),
        typeof(SetFeedbackStarsCommandStep),
        typeof(SetIsAnonymousFeedbackCommandStep)
    };
}