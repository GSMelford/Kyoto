using Kyoto.Domain.CommandSystem.Interfaces;
using Kyoto.Domain.Menu.Interfaces;
using Kyoto.Domain.PreparedMessagesSystem;
using Kyoto.Domain.Processors.Interfeces;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Services.Processors;

public class MessageService : IMessageService
{
    private readonly ICommandService _commandService;
    private readonly IMenuService _menuService;
    private readonly IPreparedMessagesService _preparedMessagesService;

    public MessageService(
        ICommandService commandService,
        IMenuService menuService,
        IPreparedMessagesService preparedMessagesService)
    {
        _commandService = commandService;
        _menuService = menuService;
        _preparedMessagesService = preparedMessagesService;
    }

    public virtual async Task ProcessAsync(Session session, Message message)
    {
        var text = message.Text!; 
        var (isExist, command) = await _menuService.TryGetMenuCommandCodeAsync(text);

        if (isExist) {
            await _commandService.ProcessCommandAsync(session, command, message);
        }
        else {
            await _commandService.ProcessCommandAsync(session, text, message);
        }

        await _preparedMessagesService.ProcessAsync(session, text);
        await _menuService.SendMenuIfExistsAsync(session, message.Text!);
    }
}