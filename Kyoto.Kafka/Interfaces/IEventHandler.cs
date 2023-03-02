namespace Kyoto.Kafka.Interfaces;

public interface IEventHandler<in TEvent>
{
    Task HandleAsync(TEvent @event);
}