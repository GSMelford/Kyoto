using Kyoto.Bot.Services.RequestSender;
using Kyoto.Domain.BotUser;
using Kyoto.Domain.CommandServices;
using Kyoto.Domain.ExecutiveCommand;
using Kyoto.Domain.RequestSender;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;

namespace Kyoto.Bot.CommandServices;

public class StartCommandService : IStartCommandService
{
    private readonly ILogger<IStartCommandService> _logger;
    private readonly IRequestService _requestService;
    private readonly IUserRepository _userRepository;
    private readonly IExecutiveTelegramCommandRepository _executiveTelegramCommandRepository;

    public StartCommandService(
        ILogger<IStartCommandService> logger,
        IRequestService requestService,
        IUserRepository userRepository, 
        IExecutiveTelegramCommandRepository executiveTelegramCommandRepository)
    {
        _logger = logger;
        _requestService = requestService;
        _userRepository = userRepository;
        _executiveTelegramCommandRepository = executiveTelegramCommandRepository;
    }

    public async Task ExecuteAsync(Guid sessionId, long chatId, long telegramId)
    {
        Request request;
        if (await _userRepository.IsUserExistAsync(telegramId))
        {
            request = new SendMessageRequest(new SendMessageParameters
            {
                Text = "Welcome back!☺️",
                ChatId = chatId
            }).ToDomain();
        }
        else
        {
            request = new SendMessageRequest(new SendMessageParameters
            {
                Text = "Hello! We haven't met before, let's get to know each other 👉👈⬇️",
                ChatId = chatId,
                ReplyMarkup = new ReplyKeyboardMarkup { OneTimeKeyboard = true }
                    .Add(new KeyboardButton
                    {
                        Text = "Register - and start creating your bot! 😎",
                        RequestContact = true
                    })
            }).ToDomain();

            bool isSavedCommand = await _executiveTelegramCommandRepository.SaveExecutiveCommandAsync(telegramId, ExecutiveCommandType.Register);
            if (!isSavedCommand) {
                request = new SendMessageRequest(new SendMessageParameters
                {
                    Text = "Click the button below to register!⬇️\nI don't talk to strangers 😶‍🌫️",
                    ChatId = chatId
                }).ToDomain();
            }
            
            _logger.LogInformation("{CommandService}. New user. SessionId: {SessionId}",
                nameof(StartCommandService), sessionId);
        }

        await _requestService.SendRequestAsync(sessionId, request);
    }
}