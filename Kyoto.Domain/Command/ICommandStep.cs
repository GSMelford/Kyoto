namespace Kyoto.Domain.Command;

public interface ICommandStep
{
    Task SendActionRequestAsync();
    Task ProcessResponseAsync();
    Task SendRetryActionRequestAsync();
}