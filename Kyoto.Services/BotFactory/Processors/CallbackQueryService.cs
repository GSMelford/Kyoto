using Kyoto.Domain.CommandSystem.Interfaces;
using Kyoto.Domain.Processors.Interfeces;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Services.BotFactory.Processors;

public class CallbackQueryService : ICallbackQueryService
{
    private readonly ICommandService _commandService;

    public CallbackQueryService(ICommandService commandService)
    {
        _commandService = commandService;
    }
    
    public Task ProcessAsync(Session session, CallbackQuery callbackQuery)
    {
        return _commandService.ProcessCommandAsync(session, callbackQuery);
    }
}