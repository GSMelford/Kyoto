using TBot.Core.RequestArchitecture;
using Request = Kyoto.Domain.RequestSender.Request;

namespace Kyoto.Bot.Services.RequestSender;

public static class Converter
{
    public static Request ToDomain(this BaseRequest baseRequest)
    {
        return new Request(
            baseRequest.Endpoint,
            baseRequest.Method,
            baseRequest.Headers?.ToDictionary(x => x.Key, y => y.Value),
            baseRequest.Parameters?.ToDictionary(x => x.Key, y => y.Value?.ToString())!);
    }
}