using Kyoto.Bot.Services.RequestSender;
using Kyoto.Domain.PostSystem;
using Kyoto.Domain.Processors;
using Kyoto.Domain.System;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;

namespace Kyoto.Bot.Services.Bot;

public class BotPostService
{
    private readonly IPostService _postService;

    public BotPostService(IPostService postService)
    {
        _postService = postService;
    }

    public Task SendMessageSuccessfulRegistrationAsync(Session session)
    {
        return _postService.SendAsync(session.Id, new SendMessageRequest(new SendMessageParameters
        {
            Text = "🎉🎊 Bot successfully registered! Let's give him a short code name",
            ChatId = session.ChatId
        }).ToDomain());
    }
    
    public Task SendUpdateBotNameConfirmationRequestAsync(Session session, string botName)
    {
        return _postService.SendAsync(session.Id, new SendMessageRequest(new SendMessageParameters
        {
            Text = $"⚠️ Are you sure you want to name the bot like {botName.ToLower()}?",
            ChatId = session.ChatId,
            ReplyMarkup = new InlineKeyboardMarkup()
                .Add(new InlineKeyboardButton
                {
                    Text = CallbackQueryButtons.Confirmation,
                    CallbackData = botName
                })
                .Add(new InlineKeyboardButton
                {
                    Text = CallbackQueryButtons.Cancel,
                    CallbackData = botName
                })
        }).ToDomain());
    }

    public Task SendMessageSuccessfulFullyRegistrationAsync(Session session)
    {
        return _postService.SendAsync(session.Id, new SendMessageRequest(new SendMessageParameters
        {
            Text = "Your bot is fully registered in the system. Let's run it 🚀",
            ChatId = session.ChatId
        }).ToDomain());
    }
}