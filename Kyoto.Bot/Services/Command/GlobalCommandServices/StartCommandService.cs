using Kyoto.Domain.BotUser;
using Kyoto.Domain.Command;
using Kyoto.Domain.Command.GlobalCommand;
using Kyoto.Domain.PostSystem;
using Kyoto.Domain.System;

namespace Kyoto.Bot.Services.Command.GlobalCommandServices;

public class StartCommandService : IStartCommandService
{
    private readonly ILogger<IStartCommandService> _logger;
    private readonly IPostService _postService;
    private readonly IUserRepository _userRepository;
    private readonly IExecutiveCommandService _executiveCommandService;

    public StartCommandService(
        ILogger<IStartCommandService> logger,
        IPostService postService, 
        IUserRepository userRepository, 
        IExecutiveCommandService executiveCommandService)
    {
        _logger = logger;
        _postService = postService;
        _userRepository = userRepository;
        _executiveCommandService = executiveCommandService;
    }

    public async Task ExecuteAsync(Session session)
    {
        if (await _userRepository.IsUserExistAsync(session.ExternalUserId))
        {
            await _postService.SendTextMessageAsync(session, "Welcome back!☺️");
        }
        else
        {
            await _executiveCommandService.StartExecutiveCommandAsync(session, CommandType.Registration);
            _logger.LogInformation("{CommandService}. New user. SessionId: {SessionId}",
                nameof(StartCommandService), session.Id);
        }
    }
}