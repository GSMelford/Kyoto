using Kyoto.Domain.BotFactory.Bot.Interfaces;
using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Services.BotFactory.PostSystem;
using Kyoto.Services.CommandSystem;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;

namespace Kyoto.Commands.DisableBotCommand;

public class DisableBotCommandStep : BaseCommandStep
{
    private readonly IBotRepository _botRepository;
    private readonly IPostService _postService;
    private readonly IBotService _botService;

    public DisableBotCommandStep(IBotRepository botRepository, IPostService postService, IBotService botService)
    {
        _botRepository = botRepository;
        _postService = postService;
        _botService = botService;
    }

    protected override async Task<CommandStepResult> SetActionRequestAsync()
    {
        var keyboard = new InlineKeyboardMarkup();
        var botList = await _botRepository.GetBotsAsync(Session.ExternalUserId, true);
        
        if (!botList.Any()) {
            await _postService.SendTextMessageAsync(Session, "На даний момент у вас немає активних ботів");
            return CommandStepResult.CreateInterrupt();
        }
        
        foreach (var botName in botList)
        {
            keyboard.Add(new InlineKeyboardButton
            {
                Text = botName,
                CallbackData = botName
            });
        }

        await _postService.PostAsync(Session, new SendMessageRequest(new SendMessageParameters
        {
            Text = "Виберіть бота для деактивації:",
            ReplyMarkup = keyboard,
            ChatId = Session.ChatId
        }).ToRequest());
        
        return CommandStepResult.CreateSuccessful();
    }

    protected override async Task<CommandStepResult> SetProcessResponseAsync()
    {
        if (CommandContext.CallbackQuery is null)
        {
            return CommandStepResult.CreateRetry();
        }

        var botName = CommandContext.CallbackQuery.Data!;
        await _postService.SendTextMessageAsync(Session, 
            $"😴 Вимкнення {botName}... Біп біп біп...");
        
        await _botService.DeactivateBotAsync(Session, botName);
        return CommandStepResult.CreateSuccessful();
    }
}