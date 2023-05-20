using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.CommandSystem.Interfaces;
using Kyoto.Domain.System;

namespace Kyoto.Services.CommandSystem;

public abstract class BaseCommandStep : ICommandStep
{
    protected Session Session { get; private set; } = null!;
    public CommandContext CommandContext { get; private set; } = null!;
    
    public void SetSession(Session session)
    {
        Session = session;
    }
    
    public void SetCommandContext(CommandContext commandContext)
    {
        CommandContext = commandContext;
    }
    
    protected abstract Task<CommandStepResult> SetActionRequestAsync();
    protected abstract Task<CommandStepResult> SetProcessResponseAsync();
    protected virtual Task<CommandStepResult> SetRetryActionRequestAsync()
    {
        return SetActionRequestAsync();
    }

    public async Task<CommandStepResult> SendActionRequestAsync()
    {
        try
        {
            return await SetActionRequestAsync();
        }
        catch
        {
            return CommandStepResult.CreateInterrupt();
        }
    }

    public async Task<CommandStepResult> ProcessResponseAsync()
    {
        try
        {
            return await SetProcessResponseAsync();
        }
        catch
        {
            return CommandStepResult.CreateRetry();
        }
    }
    
    public async Task<CommandStepResult> SendRetryActionRequestAsync()
    {
        try
        {
            return await SetRetryActionRequestAsync();
        }
        catch
        {
            return CommandStepResult.CreateInterrupt();
        }
    }
}