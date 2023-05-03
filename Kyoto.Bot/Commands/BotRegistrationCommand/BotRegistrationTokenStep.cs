using Kyoto.Bot.Commands.ExecutiveCommandSystem;
using Kyoto.Domain.Bot;
using Kyoto.Domain.PostSystem;
using Newtonsoft.Json;

namespace Kyoto.Bot.Commands.BotRegistrationCommand;

public class BotRegistrationTokenStep : BaseCommandStep
{
    private readonly IPostService _postService;

    public BotRegistrationTokenStep(IPostService postService)
    {
        _postService = postService;
    }
    
    public override async Task SendActionRequestAsync()
    {
        await _postService.SendTextMessageAsync(CommandContext.Session, 
            "🔑 First of all, generate and send us a bot token:");
    }

    public override Task ProcessResponseAsync()
    {
        CommandContext.SetAdditionalData(JsonConvert.SerializeObject(BotModel.CreateWithOnlyToken(CommandContext.Message!.Text!)));
        return Task.CompletedTask;
    }
}