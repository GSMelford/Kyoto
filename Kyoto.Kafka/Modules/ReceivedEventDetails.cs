using Confluent.Kafka;

namespace Kyoto.Kafka.Events;

public class ReceivedEventDetails
{ 
    public string Topic { get; }
    public string Message { get; }
    public ConsumeResult<Ignore, string> ConsumeResult { get; }
    
    public ReceivedEventDetails(string topic, string message, ConsumeResult<Ignore, string> consumeResult)
    {
        Topic = topic;
        Message = message;
        ConsumeResult = consumeResult;
    }
}