using Kyoto.Domain.System;
using Kyoto.Telegram.Sender.Domain;

namespace Kyoto.Telegram.Sender.Interfaces;

public interface IRequestService
{
    Task<HttpResponseMessage> SendAsync(Session session, RequestModel requestModel);
}