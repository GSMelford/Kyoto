using Kyoto.Domain.BotFactory.Bot;
using Kyoto.Domain.BotFactory.Bot.Interfaces;
using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.Processors;
using Kyoto.Services.CommandSystem;
using Kyoto.Services.HttpServices.BotRegistration;
using Newtonsoft.Json;

namespace Kyoto.Commands.BotFactory.BotRegistrationCommand;

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

    protected override async Task<CommandStepResult> SetActionRequestAsync()
    {
        var botModel = JsonConvert.DeserializeObject<BotModel>(CommandContext.AdditionalData!)!;
        botModel = await _botRegistrationHttpService.GetBotInfoAsync(botModel);
        await _postService.SendConfirmationMessageAsync(Session, BuildConfirmNameQuestion(botModel));
        CommandContext.SetAdditionalData(JsonConvert.SerializeObject(botModel));
        return CommandStepResult.CreateSuccessful();
    }

    protected override async Task<CommandStepResult> SetProcessResponseAsync()
    {
        var botModel = JsonConvert.DeserializeObject<BotModel>(CommandContext.AdditionalData!)!;
        
        if (CommandContext.CallbackQuery!.Data == CallbackQueryButtons.Confirmation)
        {
            await _postService.DeleteMessageAsync(Session);
            await _postService.SendTextMessageAsync(Session, 
                $"{BuildConfirmNameQuestion(botModel)}\nAnswer: {CallbackQueryButtons.Confirmation}");
            
            await _botService.SaveAsync(Session, botModel);
            await _postService.SendTextMessageAsync(Session, 
                "The bot has been successfully registered! 🪄🥰");
            return CommandStepResult.CreateSuccessful();
        }

        return CommandStepResult.CreateRetry();
    }

    protected override async Task<CommandStepResult> SetRetryActionRequestAsync()
    {
        var botModel = JsonConvert.DeserializeObject<BotModel>(CommandContext.AdditionalData!)!;
        
        await _postService.DeleteMessageAsync(Session);
        await _postService.SendTextMessageAsync(Session, 
            $"{BuildConfirmNameQuestion(botModel)}\nAnswer: {CallbackQueryButtons.Cancel}");
        return CommandStepResult.CreateSuccessful();
    }
}