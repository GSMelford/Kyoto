using Kyoto.Domain.System;

namespace Kyoto.Domain.PostSystem.Interfaces;

public interface IPostService
{
    Task DeleteMessageAsync(Session session);
    Task SendTextMessageAsync(Session session, string text, ReturnResponseDetails? returnResponseDetails = null);
    Task SendConfirmationMessageAsync(Session session, string text, ReturnResponseDetails? returnResponseDetails = null);
    Task PostAsync(Session session, Request request, ReturnResponseDetails? returnResponseDetails = null);
}