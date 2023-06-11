using Kyoto.Domain.CommandSystem;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.StatisticCommand;

public class StatisticCommandStepFactory : BaseCommandStepFactory
{
    public override string CommandName => CommandCodes.BotFeatures.GetStatistics;

    protected override List<Type> CommandStepTypes { get; set; } = new()
    {
        typeof(GetStatisticCommandStep)
    };
}