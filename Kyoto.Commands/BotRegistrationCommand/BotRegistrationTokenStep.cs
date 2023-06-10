using Kyoto.Domain.BotFactory.Bot;
using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Services.CommandSystem;
using Newtonsoft.Json;

namespace Kyoto.Commands.BotRegistrationCommand;

public class BotRegistrationTokenStep : BaseCommandStep
{
    private readonly IPostService _postService;

    public BotRegistrationTokenStep(IPostService postService)
    {
        _postService = postService;
    }

    protected override async Task<CommandStepResult> SetActionRequestAsync()
    { 
        await _postService.SendTextMessageAsync(Session,
            "🔑 First of all, generate and send us a bot token:");
       return CommandStepResult.CreateSuccessful();
    }

    protected override Task<CommandStepResult> SetProcessResponseAsync()
    {
        CommandContext.SetAdditionalData(JsonConvert.SerializeObject(BotModel.CreateWithOnlyToken(CommandContext.Message!.Text!)));
        return Task.FromResult(CommandStepResult.CreateSuccessful());
    }
}