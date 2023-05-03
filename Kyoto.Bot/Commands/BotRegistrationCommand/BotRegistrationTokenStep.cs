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

    protected override Task SetActionRequestAsync()
    {
       return _postService.SendTextMessageAsync(CommandContext.Session, 
           "🔑 First of all, generate and send us a bot token:");
    }

    protected override Task SetProcessResponseAsync()
    {
        CommandContext.SetAdditionalData(JsonConvert.SerializeObject(BotModel.CreateWithOnlyToken(CommandContext.Message!.Text!)));
        return Task.CompletedTask;
    }
}