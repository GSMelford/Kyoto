using Kyoto.Domain.CommandSystem;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.PreOrderServiceCommand;

public class PreOrderServiceCommandStepFactory : BaseCommandStepFactory
{
    public override string CommandName => CommandCodes.BotFeatures.PreOrderService;
    protected override List<Type> CommandStepTypes { get; set; }
}