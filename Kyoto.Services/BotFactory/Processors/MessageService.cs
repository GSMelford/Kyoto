using Kyoto.Domain.BotFactory.Menu.Interfaces;
using Kyoto.Domain.ExecutiveCommand.Interfaces;
using Kyoto.Domain.Processors.Interfeces;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Services.BotFactory.Processors;

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
        await _executiveCommandService.ProcessExecutiveCommandIfExistsAsync(session, message);
        await _menuService.DrawMenuIfExist(session, message.Text!);
    }
}