using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.CommandSystem.Interfaces;
using Kyoto.Domain.Menu.Interfaces;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.Processors.Interfeces;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;
using Kyoto.Services.BotFactory.PostSystem;
using TBot.Client.Parameters;
using TBot.Client.Requests;

namespace Kyoto.Services.Processors;

public class MessageService : IMessageService
{
    private readonly ICommandService _commandService;
    private readonly IMenuService _menuService;
    private readonly IPostService _postService;

    public MessageService(ICommandService commandService, IMenuService menuService, IPostService postService)
    {
        _commandService = commandService;
        _menuService = menuService;
        _postService = postService;
    }

    public async Task ProcessAsync(Session session, Message message)
    {
        var text = message.Text!; 
        var (isExist, command) = await _menuService.TryGetMenuCommandCodeIfExists(session, text);

        if (isExist) {
            await _commandService.ProcessCommandAsync(session, command, message);
        }
        else if(CommandCodes.Cancel == text)
        {
            var canceledCommand = await _commandService.CancelCommandAsync(session);
            await _postService.PostAsync(session, new SendMessageRequest(new SendMessageParameters
            {
                Text = $"😶‍🌫️ Command {canceledCommand} was interrupted",
                ChatId = session.ChatId
            }).ToRequest());
        }
        else {
            await _commandService.ProcessCommandAsync(session, text, message);
        }
        
        await _menuService.SendMenuIfExistsAsync(session, message.Text!);
    }
}