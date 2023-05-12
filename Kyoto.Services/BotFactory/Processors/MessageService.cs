using Kyoto.Domain.BotFactory.Menu.Interfaces;
using Kyoto.Domain.CommandSystem.Interfaces;
using Kyoto.Domain.Processors.Interfeces;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Services.BotFactory.Processors;

public class MessageService : IMessageService
{
    private readonly ICommandService _commandService;
    private readonly IMenuService _menuService;

    public MessageService(ICommandService commandService, IMenuService menuService)
    {
        _commandService = commandService;
        _menuService = menuService;
    }

    public async Task ProcessAsync(Session session, Message message)
    {
        await _commandService.ProcessCommandAsync(session, message);
        await _menuService.DrawMenuIfExist(session, message.Text!);
    }
}