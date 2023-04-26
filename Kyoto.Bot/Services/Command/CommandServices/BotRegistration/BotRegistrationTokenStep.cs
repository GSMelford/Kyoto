using Kyoto.Domain.Bot;
using Kyoto.Domain.Command;
using Kyoto.Domain.PostSystem;

namespace Kyoto.Bot.Services.Command.CommandServices.BotRegistration;

public class BotRegistrationTokenStep : ICommandStep
{
    private readonly IPostService _postService;
    private readonly IBotService _botService;

    public BotRegistrationTokenStep(IPostService postService, IBotService botService)
    {
        _postService = postService;
        _botService = botService;
    }
    
    public Task SendActionRequestAsync(CommandContext commandContext)
    {
        return _postService.SendTextMessageAsync(commandContext.Session, "🪄 Send the bot token with the following message");
    }

    public async Task<CommandStepResult> ProcessResponseAsync(CommandContext commandContext)
    {
        var botId= await _botService.SaveAsync(commandContext.Session, commandContext.Message!.Text!);
        return CommandStepResult.CreateSuccessful(botId);
    }
    
    public Task FinalAction(CommandContext commandContext)
    {
        return _postService.SendTextMessageAsync(commandContext.Session, "Your token has been successfully saved!");
    }
}