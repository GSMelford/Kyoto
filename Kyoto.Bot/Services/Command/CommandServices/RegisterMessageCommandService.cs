using Kyoto.Bot.Services.Authorization;
using Kyoto.Bot.Services.Menu;
using Kyoto.Domain.Authorization;
using Kyoto.Domain.Command;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Bot.Services.Command.CommandServices;

public class RegisterMessageCommandService : IMessageCommandService
{
    private readonly IAuthorizationService _authorizationService;
    private readonly AuthorizationPostService _authorizationPostService;
    private readonly MenuPanelPostService _menuPanelPostService;

    public ExecutiveCommandType ExecutiveCommandType => ExecutiveCommandType.Register;

    public RegisterMessageCommandService(
        IAuthorizationService authorizationService,
        AuthorizationPostService authorizationPostService, 
        MenuPanelPostService menuPanelPostService)
    {
        _authorizationService = authorizationService;
        _authorizationPostService = authorizationPostService;
        _menuPanelPostService = menuPanelPostService;
    }

    public async Task ExecuteAsync(Session session, MessageCommandData messageCommandData)
    {
        var user = messageCommandData.Message.ToUserDomain();
        await _authorizationService.RegisterAsync(user);
        await _authorizationPostService.SendAfterRegistrationMessageAsync(session, user.FirstName);
        await _menuPanelPostService.SendBotManagementAsync(session);
    }
}