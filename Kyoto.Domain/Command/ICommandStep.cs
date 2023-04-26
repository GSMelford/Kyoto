namespace Kyoto.Domain.Command;

public interface ICommandStep
{
    Task SendActionRequestAsync(CommandContext commandContext);
    Task<CommandStepResult> ProcessResponseAsync(CommandContext commandContext);

    Task FinalAction(CommandContext commandContext)
    {
        return Task.CompletedTask;
    }
}