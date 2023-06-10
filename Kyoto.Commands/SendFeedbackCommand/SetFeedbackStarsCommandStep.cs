using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Extensions;
using Kyoto.Services.BotFactory.PostSystem;
using Kyoto.Services.CommandSystem;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;

namespace Kyoto.Commands.SendFeedbackCommand;

public class SetFeedbackStarsCommandStep : BaseCommandStep
{
    private readonly IPostService _postService;

    public SetFeedbackStarsCommandStep(IPostService postService)
    {
        _postService = postService;
    }

    protected override async Task<CommandStepResult> SetActionRequestAsync()
    {
        var keyboard = new InlineKeyboardMarkup();
        
        keyboard.Add(new InlineKeyboardButton { Text = "⭐️", CallbackData = "1"});
        keyboard.Add(new InlineKeyboardButton { Text = "⭐️", CallbackData = "2"});
        keyboard.Add(new InlineKeyboardButton { Text = "⭐️", CallbackData = "3"});
        keyboard.Add(new InlineKeyboardButton { Text = "⭐️", CallbackData = "4"});
        keyboard.Add(new InlineKeyboardButton { Text = "🌟", CallbackData = "5"});

        await _postService.PostAsync(Session, new SendMessageRequest(new SendMessageParameters
        {
            Text = "Скільки зірок Ви поставите цьому боту?",
            ChatId = Session.ChatId,
            ReplyMarkup = keyboard
        }).ToRequest());

        return CommandStepResult.CreateSuccessful();
    }

    protected override Task<CommandStepResult> SetProcessResponseAsync()
    {
        var feedbackData = CommandContext.AdditionalData!.ToObject<FeedbackData>();
        feedbackData.Stars = int.Parse(CommandContext.CallbackQuery!.Data!);
        CommandContext.SetAdditionalData(feedbackData.ToJson());
        return Task.FromResult(CommandStepResult.CreateSuccessful());
    }
}