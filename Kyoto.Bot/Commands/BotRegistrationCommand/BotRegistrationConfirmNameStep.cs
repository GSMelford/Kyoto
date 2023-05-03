using Kyoto.Bot.Commands.ExecutiveCommandSystem;
using Kyoto.Bot.HttpServices.BotRegistration;
using Kyoto.Domain.Bot;
using Kyoto.Domain.PostSystem;
using Kyoto.Domain.Processors;
using Newtonsoft.Json;

namespace Kyoto.Bot.Commands.BotRegistrationCommand;

public class BotRegistrationConfirmNameStep : BaseCommandStep
{
    private readonly IPostService _postService;
    private readonly IBotService _botService;

    private readonly BotRegistrationHttpService _botRegistrationHttpService;

    private static string BuildConfirmNameQuestion(BotModel botModel) => 
        $"🤔 Do you want to register this bot?\n\n" +
        $"Name: {botModel.FirstName}\n" +
        $"Username: {botModel.Username}";

    public BotRegistrationConfirmNameStep(
        IPostService postService, 
        IBotService botService, 
        BotRegistrationHttpService botRegistrationHttpService)
    {
        _postService = postService;
        _botService = botService;
        _botRegistrationHttpService = botRegistrationHttpService;
    }
    
    public override async Task SendActionRequestAsync()
    {
        var botModel = JsonConvert.DeserializeObject<BotModel>(CommandContext.AdditionalData!)!;
        botModel = await _botRegistrationHttpService.GetBotInfoAsync(botModel);
        await _postService.SendConfirmationMessageAsync(CommandContext.Session, BuildConfirmNameQuestion(botModel));
        CommandContext.SetAdditionalData(JsonConvert.SerializeObject(botModel));
    }

    public override async Task ProcessResponseAsync()
    {
        var botModel = JsonConvert.DeserializeObject<BotModel>(CommandContext.AdditionalData!)!;
        
        if (CommandContext.CallbackQuery!.Data == CallbackQueryButtons.Confirmation)
        {
            await _postService.DeleteMessageAsync(CommandContext.Session);
            await _postService.SendTextMessageAsync(CommandContext.Session, 
                $"{BuildConfirmNameQuestion(botModel)}\nAnswer: {CallbackQueryButtons.Confirmation}");
            
            await _botService.SaveAsync(CommandContext.Session, botModel);
            await _postService.SendTextMessageAsync(CommandContext.Session, 
                "The bot has been successfully registered! 🪄🥰");
        }
        else {
            await _postService.DeleteMessageAsync(CommandContext.Session);
            await _postService.SendTextMessageAsync(CommandContext.Session, 
                $"{BuildConfirmNameQuestion(botModel)}\nAnswer: {CallbackQueryButtons.Cancel}");
            CommandContext.SetRetry();
        }
    }
}