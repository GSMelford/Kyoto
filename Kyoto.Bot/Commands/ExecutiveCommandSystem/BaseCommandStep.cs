using Kyoto.Domain.Command;

namespace Kyoto.Bot.Commands.ExecutiveCommandSystem;

public abstract class BaseCommandStep : ICommandStep
{
    public CommandContext CommandContext { get; private set; } = null!;

    public void SetCommandContext(CommandContext commandContext)
    {
        CommandContext = commandContext;
    }

    public abstract Task SendActionRequestAsync();
    public abstract Task ProcessResponseAsync();
}