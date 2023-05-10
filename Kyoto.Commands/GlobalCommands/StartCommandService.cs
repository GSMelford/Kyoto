using Kyoto.Domain.BotFactory.GlobalCommand.Interfaces;
using Kyoto.Domain.BotFactory.User.Interfaces;
using Kyoto.Domain.ExecutiveCommand.Interfaces;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.System;
using Microsoft.Extensions.Logging;

namespace Kyoto.Commands.GlobalCommands;

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

    public async Task ExecuteAsync(Session session, string firstName)
    {
        if (await _userRepository.IsUserExistAsync(session.ExternalUserId))
        {
            await _postService.SendTextMessageAsync(session, "Welcome back!☺️");
        }
        else
        {
            await _executiveCommandService.StartExecutiveCommandAsync(session, "Registration", firstName);
            _logger.LogInformation("{CommandService}. New user. SessionId: {SessionId}",
                nameof(StartCommandService), session.Id);
        }
    }
}