using Kyoto.Domain.PostSystem;
using Newtonsoft.Json;

namespace Kyoto.Bot.Services.Command.CommandServices.BotRegistration;

public class BotRegistrationNameStep : BaseCommandStep
{
    private readonly IPostService _postService;

    public BotRegistrationNameStep(IPostService postService)
    {
        _postService = postService;
    }
    
    public override async Task SendActionRequestAsync()
    {
        await _postService.SendTextMessageAsync(CommandContext.Session,
            "🤔 Think of a short name for the bot (4 - 15 characters, for example: kyoto_factory)");
    }

    public override Task ProcessResponseAsync()
    {
        var botRegistrationData = JsonConvert.DeserializeObject<BotRegistrationData>(CommandContext.AdditionalData!);
        botRegistrationData!.Name = CommandContext.Message!.Text!;
        CommandContext.SetAdditionalData(JsonConvert.SerializeObject(botRegistrationData));
        return Task.FromResult(CommandContext);
    }
}