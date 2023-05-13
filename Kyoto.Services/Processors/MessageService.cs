using Kyoto.Domain.CommandSystem.Interfaces;
using Kyoto.Domain.Menu.Interfaces;
using Kyoto.Domain.Processors.Interfeces;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Services.Processors;

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
        var text = message.Text!; 
        var (isExist, command) = await _menuService.TryGetMenuCommandCodeIfExists(session, text);

        if (isExist) {
            await _commandService.ProcessCommandAsync(session, command, message);
        }
        else {
            await _commandService.ProcessCommandAsync(session, text, message);
        }
        
        await _menuService.SendMenuIfExistsAsync(session, message.Text!);
    }
}