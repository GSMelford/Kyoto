using Kyoto.Bot.Services.RequestSender;
using Kyoto.Domain.PostSystem;
using Kyoto.Domain.System;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters;
using TBot.Client.Requests;

namespace Kyoto.Bot.Services.Authorization;

public class AuthorizationPostService
{
    private readonly IPostService _postService;

    public AuthorizationPostService(IPostService postService)
    {
        _postService = postService;
    }

    public async Task SendAfterRegistrationMessageAsync(Session session, string firstName)
    {
        await _postService.SendAsync(session.Id, new SendMessageRequest(new SendMessageParameters
        {
            Text = $"Thank you for registering, {firstName}! 💞",
            ChatId = session.ChatId,
            ReplyMarkup = new ReplyKeyboardRemove
            {
                RemoveKeyboard = true
            }
        }).ToDomain());
    }
}