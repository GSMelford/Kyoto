using Kyoto.Domain.Command;
using Kyoto.Domain.Processors;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Bot.Services.Processors;

public class CallbackQueryService : ICallbackQueryService
{
    private readonly IExecutiveCommandService _executiveCommandService;

    public CallbackQueryService(IExecutiveCommandService executiveCommandService)
    {
        _executiveCommandService = executiveCommandService;
    }
    
    public Task ProcessAsync(Session session, CallbackQuery callbackQuery)
    {
        return _executiveCommandService.ProcessExecutiveCommandIfExistAsync(session, callbackQuery: callbackQuery);
    }
}