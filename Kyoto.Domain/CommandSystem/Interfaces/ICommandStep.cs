namespace Kyoto.Domain.CommandSystem.Interfaces;

public interface ICommandStep
{
    Task<CommandStepResult> SendActionRequestAsync();
    Task<CommandStepResult> ProcessResponseAsync();
    Task<CommandStepResult> SendRetryActionRequestAsync();
}