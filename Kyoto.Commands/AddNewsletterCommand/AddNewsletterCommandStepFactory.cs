using Kyoto.Commands.CommonSteps;
using Kyoto.Domain.CommandSystem;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.AddNewsletterCommand;

public class AddNewsletterCommandStepFactory : BaseCommandStepFactory
{
    public override string CommandName => CommandCodes.BotFeatures.AddNewsletter;

    protected override List<Type> CommandStepTypes { get; set; } = new ()
    {
        typeof(SelectBotCommandStep),
        typeof(SetTextNewsletterCommandStep),
        typeof(MakingChoiceNewsletterEventCommandStep),
        typeof(SetUpNewsletterEventCommandStep)
    };
}