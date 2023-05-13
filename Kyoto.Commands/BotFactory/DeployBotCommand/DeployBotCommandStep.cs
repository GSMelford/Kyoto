using Kyoto.Domain.BotFactory.Bot.Interfaces;
using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Services.BotFactory.PostSystem;
using Kyoto.Services.CommandSystem;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;

namespace Kyoto.Commands.BotFactory.DeployBotCommand;

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
        var botList = await _botRepository.GetBotListAsync(Session.ExternalUserId);
        
        if (!botList.Any()) {
            await _postService.SendTextMessageAsync(Session, "You don't have bots yet. Register first!");
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
            Text = "Choose the bot you want to run:",
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
            $"🪄 Let's start deploying the {botName}... 5, 4, 3, 2, 1!!💥");
        await _botService.ActivateBotAsync(Session, botName);
        
        return CommandStepResult.CreateSuccessful();
    }
}