using Kyoto.Domain.Tenant.Interfaces;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Kafka.Handlers;

public class DeployStatusHandler : IKafkaHandler<DeployStatusEvent>
{
    private readonly IDeployStatusService _deployStatusService;

    public DeployStatusHandler(IDeployStatusService deployStatusService)
    {
        _deployStatusService = deployStatusService;
    }

    public Task HandleAsync(DeployStatusEvent deployStatusEvent)
    {
        return _deployStatusService.Notify(deployStatusEvent.GetSession());
    }
}