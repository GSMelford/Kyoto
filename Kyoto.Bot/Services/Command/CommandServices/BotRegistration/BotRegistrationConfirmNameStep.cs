using Kyoto.Domain.Bot;
using Kyoto.Domain.Command;
using Kyoto.Domain.PostSystem;
using Kyoto.Domain.Processors;

namespace Kyoto.Bot.Services.Command.CommandServices.BotRegistration;

public class BotRegistrationConfirmNameStep : ICommandStep
{
    private readonly IPostService _postService;
    private readonly IBotService _botService;

    public BotRegistrationConfirmNameStep(IPostService postService, IBotService botService)
    {
        _postService = postService;
        _botService = botService;
    }
    
    public Task SendActionRequestAsync(CommandContext commandContext)
    {
        var botName = commandContext.Message!.Text!;
        return _postService.SendConfirmationMessageAsync(commandContext.Session,
            $"Are you sure you want to name the bot {botName}?", botName);
    }

    public async Task<CommandStepResult> ProcessResponseAsync(CommandContext commandContext)
    {
        if (commandContext.CallbackQuery!.Message!.Text == CallbackQueryButtons.Confirmation)
        {
            await _botService.UpdateBotNameAsync(
                Guid.Parse(commandContext.AdditionalData!.ToString()!), commandContext.CallbackQuery.Data!);
            return CommandStepResult.CreateSuccessful();
        }
        
        return CommandStepResult.CreateRetry("You need to register first!");
    }

    public Task FinalAction(CommandContext commandContext)
    {
        return _postService.SendTextMessageAsync(
            commandContext.Session, "Thank you! The bot has been successfully registered!");
    }
}