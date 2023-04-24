using Kyoto.Bot.Services.RequestSender;
using Kyoto.Domain.BotUser;
using Kyoto.Domain.Command;
using Kyoto.Domain.PostSystem;
using Kyoto.Domain.System;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;

namespace Kyoto.Bot.Services.Command.GlobalCommandServices;

public class StartCommandService : IStartCommandService
{
    private readonly ILogger<IStartCommandService> _logger;
    private readonly IPostService _postService;
    private readonly IUserRepository _userRepository;
    private readonly IExecutiveCommandRepository _executiveCommandRepository;

    public StartCommandService(
        ILogger<IStartCommandService> logger,
        IPostService postService,
        IUserRepository userRepository, 
        IExecutiveCommandRepository executiveCommandRepository)
    {
        _logger = logger;
        _postService = postService;
        _userRepository = userRepository;
        _executiveCommandRepository = executiveCommandRepository;
    }

    public async Task ExecuteAsync(Session session)
    {
        Request request;
        if (await _userRepository.IsUserExistAsync(session.ExternalUserId))
        {
            request = new SendMessageRequest(new SendMessageParameters
            {
                Text = "Welcome back!☺️",
                ChatId = session.ChatId
            }).ToDomain();
        }
        else
        {
            request = new SendMessageRequest(new SendMessageParameters
            {
                Text = "Hello! We haven't met before, let's get to know each other 👉👈⬇️",
                ChatId = session.ChatId,
                ReplyMarkup = new ReplyKeyboardMarkup { OneTimeKeyboard = true }
                    .Add(new KeyboardButton
                    {
                        Text = "Register - and start creating your bot! 😎",
                        RequestContact = true
                    })
            }).ToDomain();

            bool isSavedCommand = await _executiveCommandRepository.SaveExecutiveCommandAsync(session, ExecutiveCommandType.Register);
            if (!isSavedCommand) {
                request = new SendMessageRequest(new SendMessageParameters
                {
                    Text = "Click the button below to register!⬇️\nI don't talk to strangers 😶‍🌫️",
                    ChatId = session.ChatId
                }).ToDomain();
            }
            
            _logger.LogInformation("{CommandService}. New user. SessionId: {SessionId}",
                nameof(StartCommandService), session.Id);
        }

        await _postService.SendAsync(session.Id, request);
    }
}