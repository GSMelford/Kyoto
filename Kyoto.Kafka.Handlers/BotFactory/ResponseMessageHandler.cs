using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Kafka.Handlers.BotFactory;

public class ResponseMessageHandler : IKafkaHandler<ResponseMessageEvent>
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