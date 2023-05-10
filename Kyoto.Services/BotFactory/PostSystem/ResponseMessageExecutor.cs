using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Kyoto.Services.BotFactory.PostSystem;

public class ResponseMessageExecutor : IResponseMessageExecutor
{
    private readonly IServiceProvider _serviceProvider;

    public ResponseMessageExecutor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task ExecuteAsync(Session session, Message message, string type)
    {
        var handlerType = Type.GetType(type);
        using var scope = _serviceProvider.CreateScope();
        var handler = ActivatorUtilities.CreateInstance(scope.ServiceProvider, handlerType!) as IResponseHandler;
        return handler!.ExecuteAsync(session, message);
    }
}