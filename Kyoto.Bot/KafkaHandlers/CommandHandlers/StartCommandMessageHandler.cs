using Kyoto.Bot.Services.RequestSender;
using Kyoto.Domain.BotPhrases;
using Kyoto.Domain.BotUser;
using Kyoto.Domain.RequestSender;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;

namespace Kyoto.Bot.KafkaHandlers.CommandHandlers;

public class StartCommandMessageHandler : IEventHandler<StartCommandEvent>
{
    private readonly ILogger<IEventHandler<StartCommandEvent>> _logger;
    private readonly IRequestService _requestService;
    private readonly IUserRepository _userRepository;

    public StartCommandMessageHandler(
        ILogger<IEventHandler<StartCommandEvent>> logger,
        IRequestService requestService,
        IUserRepository userRepository)
    {
        _logger = logger;
        _requestService = requestService;
        _userRepository = userRepository;
    }

    public async Task HandleAsync(StartCommandEvent startCommandEvent)
    {
        Request request;
        if (await _userRepository.IsUserExistAsync(startCommandEvent.TelegramUserId))
        {
            request = new SendMessageRequest(new SendMessageParameters
            {
                Text = CommandPhrases.WelcomeBack,
                ChatId = startCommandEvent.ChatId
            }).ToDomain();
        }
        else
        {
            request = new SendMessageRequest(new SendMessageParameters
            {
                Text = "Welcome!",
                ChatId = startCommandEvent.ChatId,
                ReplyMarkup = new ReplyKeyboardMarkup { OneTimeKeyboard = true }
                    .Add(new KeyboardButton
                    {
                        Text = CommandPhrases.ShareContact,
                        RequestContact = true
                    })
            }).ToDomain();

            _logger.LogInformation("{CommandHandler}. New user. SessionId: {SessionId}",
                nameof(StartCommandMessageHandler), startCommandEvent.SessionId);
        }

        await _requestService.SendRequestAsync(startCommandEvent.SessionId, request);
        _logger.LogInformation("{CommandHandler} has been processed. SessionId: {SessionId}",
            nameof(StartCommandMessageHandler), startCommandEvent.SessionId);
    }
}