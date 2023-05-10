using Kyoto.Domain.ExecutiveCommand.Interfaces;
using Kyoto.Domain.Processors.Interfeces;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Services.BotFactory.Processors;

public class CallbackQueryService : ICallbackQueryService
{
    private readonly IExecutiveCommandService _executiveCommandService;

    public CallbackQueryService(IExecutiveCommandService executiveCommandService)
    {
        _executiveCommandService = executiveCommandService;
    }
    
    public Task ProcessAsync(Session session, CallbackQuery callbackQuery)
    {
        return _executiveCommandService.ProcessExecutiveCommandIfExistsAsync(session, callbackQuery: callbackQuery);
    }
}