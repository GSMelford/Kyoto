using Kyoto.Domain.System;

namespace Kyoto.Domain.PostSystem.Interfaces;

public interface IPostService
{
    Task DeleteMessageAsync(Session session);
    Task SendTextMessageAsync(Session session, string text);
    Task SendConfirmationMessageAsync(Session session, string text);
    Task PostAsync(Session session, Request request);
    Task PostBehalfOfFactoryAsync(Session session, Request request);
}