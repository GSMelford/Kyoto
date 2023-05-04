using Kyoto.Bot.Core.ExecutiveCommandSystem.Interfaces;
using Kyoto.Domain.Menu;
using Kyoto.Domain.Processors;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Bot.Services.Processors;

public class MessageService : IMessageService
{
    private readonly IExecutiveCommandService _executiveCommandService;
    private readonly IMenuService _menuService;

    public MessageService(IExecutiveCommandService executiveCommandService, IMenuService menuService)
    {
        _executiveCommandService = executiveCommandService;
        _menuService = menuService;
    }

    public async Task ProcessAsync(Session session, Message message)
    {
        await _executiveCommandService.ProcessExecutiveCommandIfExistAsync(session, message);
        await _menuService.DrawMenuIfExist(session, message.Text!);
    }
}