using Kyoto.Bot.Commands.ExecutiveCommandSystem;
using Kyoto.Bot.Services.PostSystem;
using Kyoto.Domain.Bot;
using Kyoto.Domain.PostSystem;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;

namespace Kyoto.Bot.Commands.DeployBotCommand;

public class SelectDeployBotCommandStep : BaseCommandStep
{
    private readonly IBotService _botService;
    private readonly IBotRepository _botRepository;
    private readonly IPostService _postService;

    public SelectDeployBotCommandStep(IBotRepository botRepository, IBotService botService, IPostService postService)
    {
        _botService = botService;
        _botRepository = botRepository;
        _postService = postService;
    }

    protected override async Task SetActionRequestAsync()
    {
        var keyboard = new InlineKeyboardMarkup();
        var botList = await _botRepository.GetBotListAsync(CommandContext.Session.ExternalUserId);
        
        if (!botList.Any()) {
            await _postService.SendTextMessageAsync(CommandContext.Session, "You don't have bots yet. Register first!");
        }
        
        foreach (var botName in botList)
        {
            keyboard.Add(new InlineKeyboardButton
            {
                Text = botName,
                CallbackData = botName
            });
        }

        await _postService.PostAsync(CommandContext.Session, new SendMessageRequest(new SendMessageParameters
        {
            Text = "Choose the bot you want to run:",
            ReplyMarkup = keyboard,
            ChatId = CommandContext.Session.ChatId
        }).ToRequest());
    }

    protected override async Task SetProcessResponseAsync()
    {
        if (CommandContext.CallbackQuery is null)
        {
            CommandContext.SetRetry();
            return;
        }

        var botName = CommandContext.CallbackQuery.Data!;
        await _botService.ActivateBotAsync(CommandContext.Session, botName);
        await _postService.SendTextMessageAsync(CommandContext.Session, 
            $"Bot {botName} is activated and ready to work!");
    }
}