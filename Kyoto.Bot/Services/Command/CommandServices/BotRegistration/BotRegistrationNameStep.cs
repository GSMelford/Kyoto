using Kyoto.Domain.Bot;
using Kyoto.Domain.Command;
using Kyoto.Domain.PostSystem;

namespace Kyoto.Bot.Services.Command.CommandServices.BotRegistration;

public class BotRegistrationNameStep : ICommandStep
{
    private readonly IPostService _postService;
    private readonly IBotService _botService;

    public BotRegistrationNameStep(IPostService postService, IBotService botService)
    {
        _postService = postService;
        _botService = botService;
    }
    
    public Task SendActionRequestAsync(CommandContext commandContext)
    {
        return _postService.SendTextMessageAsync(commandContext.Session,
            "Let's come up with a short name for your bot:");
    }

    public Task<CommandStepResult> ProcessResponseAsync(CommandContext commandContext)
    {
        commandContext.SetAdditionalData(commandContext.Message!.Text!);
        return Task.FromResult(CommandStepResult.CreateSuccessful());
    }

    public Task FinalAction(CommandContext commandContext)
    {
        return _postService.SendTextMessageAsync(commandContext.Session, "Great!");
    }
}