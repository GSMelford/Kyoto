using Kyoto.Domain.Bot;
using Kyoto.Domain.Command;
using Kyoto.Domain.PostSystem;
using Kyoto.Domain.Processors;
using Newtonsoft.Json;

namespace Kyoto.Bot.Services.Command.CommandServices.BotRegistration;

public class BotRegistrationConfirmNameStep : BaseCommandStep
{
    private readonly IPostService _postService;
    private readonly IBotService _botService;

    public BotRegistrationConfirmNameStep(IPostService postService, IBotService botService)
    {
        _postService = postService;
        _botService = botService;
    }
    
    public override async Task SendActionRequestAsync()
    {
        var botRegistrationData = JsonConvert.DeserializeObject<BotRegistrationData>(CommandContext.AdditionalData!)!;
        await _postService.SendConfirmationMessageAsync(CommandContext.Session,
            $"🤖 Are you sure about this bot name as {botRegistrationData.Name}?");
    }

    public override async Task ProcessResponseAsync()
    {
        if (CommandContext.CallbackQuery!.Data == CallbackQueryButtons.Confirmation)
        {
            var botRegistrationData = JsonConvert.DeserializeObject<BotRegistrationData>(CommandContext.AdditionalData!)!;
            await _botService.SaveAsync(CommandContext.Session, botRegistrationData.Name, botRegistrationData.Token);
            await _postService.SendTextMessageAsync(CommandContext.Session, 
                "Thank you! The bot has been successfully registered!");
        }
        else {
            CommandContext.SetRetry(ExecutiveCommandStep.SecondStep);
        }
    }
}