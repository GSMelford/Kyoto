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

    public override async Task SendActionRequestAsync()
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

    public override async Task ProcessResponseAsync()
    {
        if (CommandContext.CallbackQuery is null)
        {
            CommandContext.SetRetry(
                errorMessage: "You need to choose one of the bots! Or cancel the current command: /cancel");
            return;
        }

        var botName = CommandContext.CallbackQuery.Data!;
        await _botService.ActivateBotAsync(CommandContext.Session, botName);
        await _postService.SendTextMessageAsync(CommandContext.Session, 
            $"Bot {botName} is activated and ready to work!");
    }
}