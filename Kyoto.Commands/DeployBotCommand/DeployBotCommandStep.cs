using Kyoto.Domain.BotFactory.Bot.Interfaces;
using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Services.BotFactory.PostSystem;
using Kyoto.Services.CommandSystem;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Parameters.Stickers;
using TBot.Client.Requests;
using TBot.Client.Requests.Stickers;

namespace Kyoto.Commands.DeployBotCommand;

public class DeployBotCommandStep : BaseCommandStep
{
    private readonly IBotService _botService;
    private readonly IBotRepository _botRepository;
    private readonly IPostService _postService;

    public DeployBotCommandStep(IBotRepository botRepository, IBotService botService, IPostService postService)
    {
        _botService = botService;
        _botRepository = botRepository;
        _postService = postService;
    }

    protected override async Task<CommandStepResult> SetActionRequestAsync()
    {
        var keyboard = new InlineKeyboardMarkup();
        var botList = await _botRepository.GetBotsAsync(Session.ExternalUserId, false);
        
        if (!botList.Any()) {
            await _postService.SendTextMessageAsync(Session, "У вас немає неактивних ботів");
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
            Text = "Виберіть бота, якого хочете запустити:",
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
            $"🪄 Давайте почнемо розгортати {botName.Replace("_", "\\_")}\\.\\.\\. 5, 4, 3, 2, 1\\!\\!💥");
        
        await _postService.PostAsync(Session, new SendStickerRequest(new SendStickersParameters
        {
            ChatId = Session.ChatId,
            Sticker = "CAACAgIAAxUAAWSEa3MbIwyXgtrb283Zou093NxIAAIyBwACRvusBCB5MwG6VA_qLwQ"
        }).ToRequest());
        
        await _botService.ActivateBotAsync(Session, botName);
        return CommandStepResult.CreateSuccessful();
    }
}