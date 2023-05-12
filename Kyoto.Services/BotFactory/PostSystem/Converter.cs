using TBot.Core.RequestArchitecture;
using Request = Kyoto.Domain.PostSystem.Request;

namespace Kyoto.Services.BotFactory.PostSystem;

public static class Converter
{
    public static Request ToRequest(this BaseRequest baseRequest)
    {
        return new Request(
            baseRequest.Endpoint,
            baseRequest.Method,
            baseRequest.Parameters?.ToDictionary(x => x.Key, y => y.Value?.ToString())!);
    }
}