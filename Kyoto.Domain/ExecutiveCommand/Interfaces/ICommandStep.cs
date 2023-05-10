namespace Kyoto.Domain.ExecutiveCommand.Interfaces;

public interface ICommandStep
{
    Task SendActionRequestAsync();
    Task ProcessResponseAsync();
    Task SendRetryActionRequestAsync();
}