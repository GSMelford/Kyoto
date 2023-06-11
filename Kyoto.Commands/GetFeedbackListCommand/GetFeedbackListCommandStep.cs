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

        var feedbackSet = (await response.Content.ReadAsStringAsync()).ToObject<FeedbackSet>();
        await PostListAsync(int.Parse(feedBackListData.Offset), feedbackSet);
        return CommandStepResult.CreateSuccessful();
    }

    private async Task PostListAsync(int offset, FeedbackSet feedbackSet)
    {
        string message = $"📃 Ваші відгуки {offset}-{feedbackSet.Total}\n\n{BuildMessage(feedbackSet)}";
        var keyboard = new InlineKeyboardMarkup();

        bool isButtonExist = false;
        if (offset != 0)
        {
            isButtonExist = true;
            keyboard.Add(new InlineKeyboardButton
            {
                Text = "⬅️",
                CallbackData = "⬅️"
            });
        }

        if (feedbackSet.Total - offset > 5)
        {
            isButtonExist = true;
            keyboard.Add(new InlineKeyboardButton
            {
                Text = "➡️",
                CallbackData = "➡️"
            });
        }

        if (isButtonExist)
        {
            keyboard.AddNextLine();
        }

        keyboard.Add(new InlineKeyboardButton
        {
            Text = "↙️ Вийти з перегляду",
            CallbackData = "↙️"
        });
        
        await _postService.PostAsync(Session, new SendMessageRequest(new SendMessageParameters
        {
            Text = message,
            ChatId = Session.ChatId,
            ReplyMarkup = keyboard
        }).ToRequest());
    }

    private string BuildMessage(FeedbackSet feedbackSet)
    {
        string message = string.Empty;
        if (feedbackSet.Feedbacks.Any())
        {
            foreach (var feedback in feedbackSet.Feedbacks)
            {
                message += $"{feedback.Stars} ⭐️ - {feedback.Text}";
                if (!string.IsNullOrEmpty(feedback.ClientFullName))
                {
                    message += $" - {feedback.ClientFullName}";
                }

                message += "\n";
            }
        }
        else
        {
            message = "😞 Тут пусто";
        }

        return message;
    }

    protected override async Task<CommandStepResult> SetProcessResponseAsync()
    {
        var feedBackListData = BuildFeedbackData(CommandContext.AdditionalData!);
        
        if (CommandContext.CallbackQuery!.Data == "⬅️")
        {
            var offset = int.Parse(feedBackListData.Offset);
            if (offset - 5 >= 0) {
                offset -= 5;
            }
            
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
            await _postService.PostAsync(Session, new SendMessageRequest(new SendMessageParameters
            {
                Text = CommandContext.CallbackQuery.Message!.Text!,
                ChatId = Session.ChatId
            }).ToRequest());
            
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