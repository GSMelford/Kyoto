using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.FeedbackSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.RequestSystem;
using Kyoto.Extensions;
using Kyoto.Services.BotFactory.PostSystem;
using Kyoto.Services.CommandSystem;
using Kyoto.Settings;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;

namespace Kyoto.Commands.GetFeedbackListCommand;

public class GetFeedbackListCommandStep : BaseCommandStep
{
    private readonly IPostService _postService;
    private readonly IRequestService _requestService;
    private readonly KyotoBotFactorySettings _kyotoBotFactorySettings;

    public GetFeedbackListCommandStep(
        IPostService postService, 
        IRequestService requestService, 
        KyotoBotFactorySettings kyotoBotFactorySettings)
    {
        _postService = postService;
        _requestService = requestService;
        _kyotoBotFactorySettings = kyotoBotFactorySettings;
    }

    protected override async Task<CommandStepResult> SetActionRequestAsync()
    {
        var feedBackListData = BuildFeedbackData(CommandContext.AdditionalData!);
        
        var response = await _requestService.SendAsync(new RequestCreator(HttpMethod.Get, _kyotoBotFactorySettings.ClientBaseUrl + "/api/feedback/list")
            .AddTenantHeader(feedBackListData.TenantKey)
            .AddParameters(new Dictionary<string, string>
            {
                { "offset", feedBackListData.Offset }
            })
            .Create());

        var feedback = (await response.Content.ReadAsStringAsync()).ToObject<FeedbackSet>();

        string message = string.Empty;
        if (feedback.Feedbacks.Any())
        {
            message = feedback.Feedbacks.Aggregate(message,
                (current, feedbackObject) =>
                    current + $"{feedbackObject.Stars} ⭐️ - {feedbackObject.Text} - {feedbackObject.ClientFullName}\n");
        }
        else
        {
            message = "😞 Тут пусто";
        }
        
        await _postService.PostAsync(Session, new SendMessageRequest(new SendMessageParameters
        {
            Text = message,
            ChatId = Session.ChatId,
            ReplyMarkup = new InlineKeyboardMarkup()
                .Add(new InlineKeyboardButton
                {
                    Text = "⬅️",
                    CallbackData = "⬅️"
                })
                .Add(new InlineKeyboardButton
                {
                    Text = "➡️",
                    CallbackData = "➡️"
                })
                .AddNextLine()
                .Add(new InlineKeyboardButton
                {
                    Text = "↙️ Вийти з перегляду",
                    CallbackData = "↙️"
                })
        }).ToRequest());

        return CommandStepResult.CreateSuccessful();
    }

    protected override async Task<CommandStepResult> SetProcessResponseAsync()
    {
        var feedBackListData = BuildFeedbackData(CommandContext.AdditionalData!);
        
        if (CommandContext.CallbackQuery!.Data == "⬅️")
        {
            var offset = int.Parse(feedBackListData.Offset);
            offset -= 5;
            feedBackListData.Offset = (offset).ToString();
        }
        else if (CommandContext.CallbackQuery!.Data == "➡️")
        {
            var offset = int.Parse(feedBackListData.Offset);
            offset += 5;
            feedBackListData.Offset = (offset).ToString();
        }
        else if (CommandContext.CallbackQuery!.Data == "↙️")
        {
            await _postService.DeleteMessageAsync(Session);
            await _postService.SendTextMessageAsync(Session, CommandContext.CallbackQuery.Message!.Text!.Replace("-", "\\-"));
            await _postService.SendTextMessageAsync(Session, "☕ Перегляд відгуків завершений\\!");
            return CommandStepResult.CreateSuccessful();
        }

        await _postService.DeleteMessageAsync(Session);
        CommandContext.SetAdditionalData(feedBackListData.ToJson());
        return CommandStepResult.CreateRetry();
    }
    
    private FeedbackData BuildFeedbackData(string additionalData)
    {
        FeedbackData feedBackListData;
        try
        {
            feedBackListData = CommandContext.AdditionalData!.ToObject<FeedbackData>();
        }
        catch
        {
            feedBackListData = new FeedbackData {
                Offset = 0.ToString(),
                TenantKey = additionalData
            };
        }

        return feedBackListData;
    }
}