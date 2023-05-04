namespace Kyoto.Bot.Core.ExecutiveCommandSystem.Interfaces;

public interface ICommandStep
{
    Task SendActionRequestAsync();
    Task ProcessResponseAsync();
    Task SendRetryActionRequestAsync();
}