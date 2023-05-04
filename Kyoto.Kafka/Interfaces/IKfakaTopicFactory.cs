namespace Kyoto.Kafka.Interfaces;

public interface IKfakaTopicFactory
{
    Task CreateTopicIfNotExistAsync(string topic, Dictionary<string, string> config);
}