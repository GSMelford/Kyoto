using Kyoto.Domain.PostSystem;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Bot.KafkaHandlers;

public class ResponseMessageHandler : IEventHandler<ResponseMessageEvent>
{
    private readonly IResponseMessageExecutor _responseMessageExecutor;

    public ResponseMessageHandler(IResponseMessageExecutor responseMessageExecutor)
    {
        _responseMessageExecutor = responseMessageExecutor;
    }

    public Task HandleAsync(ResponseMessageEvent responseMessageEvent)
    {
        return _responseMessageExecutor.ExecuteAsync(
            responseMessageEvent.GetSession(), 
            responseMessageEvent.Message,
            responseMessageEvent.ResponseMessageReturn.HandlerType);
    }
}