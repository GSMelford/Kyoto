using Kyoto.Domain.System;

namespace Kyoto.Domain.PostSystem;

public interface IPostService
{
    Task SendTextMessageAsync(Session session, string text);
    Task SendConfirmationMessageAsync(Session session, string text, string callbackData);
    Task SendAsync(Guid sessionId, Request request);
}