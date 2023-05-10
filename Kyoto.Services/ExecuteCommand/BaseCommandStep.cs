using Kyoto.Domain.ExecutiveCommand;
using Kyoto.Domain.ExecutiveCommand.Interfaces;

namespace Kyoto.Services.ExecuteCommand;

public abstract class BaseCommandStep : ICommandStep
{
    public CommandContext CommandContext { get; private set; } = null!;

    public void SetCommandContext(CommandContext commandContext)
    {
        CommandContext = commandContext;
    }

    protected abstract Task SetActionRequestAsync();
    protected abstract Task SetProcessResponseAsync();
    protected virtual Task SetRetryActionRequestAsync()
    {
        return SetActionRequestAsync();
    }

    public async Task SendActionRequestAsync()
    {
        try
        {
            await SetActionRequestAsync();
        }
        catch
        {
            CommandContext.SetInterrupt();
        }
    }

    public async Task ProcessResponseAsync()
    {
        try
        {
            await SetProcessResponseAsync();
        }
        catch
        {
            CommandContext.SetRetry();
        }
    }
    
    public async Task SendRetryActionRequestAsync()
    {
        try
        {
            await SetRetryActionRequestAsync();
        }
        catch
        {
            CommandContext.SetInterrupt();
        }
    }
}