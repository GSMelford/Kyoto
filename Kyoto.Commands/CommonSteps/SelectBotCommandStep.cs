using Kyoto.Domain.BotFactory.Bot.Interfaces;
using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Services.BotFactory.PostSystem;
using Kyoto.Services.CommandSystem;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;

namespace Kyoto.Commands.CommonSteps;

public class SelectBotCommandStep : BaseCommandStep
{
    private readonly IBotRepository _botRepository;
    private readonly IPostService _postService;

    public SelectBotCommandStep(IBotRepository botRepository, IPostService postService)
    {
        _botRepository = botRepository;
        _postService = postService;
    }

    protected override async Task<CommandStepResult> SetActionRequestAsync()
    {
        var keyboard = new InlineKeyboardMarkup();
        var bots = await _botRepository.GetDeployedBotsAsync(Session.ExternalUserId);
        
        if (!bots.Any()) {
            await _postService.SendTextMessageAsync(Session, "Ви ще не розгорнули жодного бота\\.%0AЩоб це зробити, зайдіть у меню\\: *🤖⚙️ Управління ботами*");
            return CommandStepResult.CreateInterrupt();
        }
        
        foreach (var botName in bots)
        {
            keyboard.Add(new InlineKeyboardButton
            {
                Text = botName,
                CallbackData = botName
            });
        }

        await _postService.PostAsync(Session, new SendMessageRequest(new SendMessageParameters
        {
            Text = "🤖 Виберіть бота:",
            ReplyMarkup = keyboard,
            ChatId = Session.ChatId
        }).ToRequest());
        
        return CommandStepResult.CreateSuccessful();
    }

    protected override async Task<CommandStepResult> SetRetryActionRequestAsync()
    {
        await _postService.PostAsync(Session, new SendMessageRequest(new SendMessageParameters
        {
            Text = "⛔ Виберіть бота, щоб продовжити, або введіть /cancel, щоб скасувати команду",
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

        CommandContext.SetAdditionalData(CommandContext.CallbackQuery.Data!);
        await _postService.PostAsync(Session, new SendMessageRequest(new SendMessageParameters
        {
            Text = $"Ваш вибір бот {CommandContext.CallbackQuery.Data!}",
            ChatId = Session.ChatId
        }).ToRequest());
        
        return CommandStepResult.CreateSuccessful();
    }
}