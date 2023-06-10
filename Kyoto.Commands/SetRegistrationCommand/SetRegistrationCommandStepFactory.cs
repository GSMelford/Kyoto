using Kyoto.Commands.CommonSteps;
using Kyoto.Domain.CommandSystem;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.SetRegistrationCommand;

public class SetRegistrationCommandStepFactory : BaseCommandStepFactory
{
    public override string CommandName => CommandCodes.BotFeatures.SetRegistration;

    protected override List<Type> CommandStepTypes { get; set; } = new()
    {
        typeof(SelectBotCommandStep),
        typeof(ChangeHelloMessageCommandStep),
        typeof(ChangeThanksRegistrationMessageCommandStep),
        typeof(ChangeRetryRegistrationMessageCommandStep)
    };
}