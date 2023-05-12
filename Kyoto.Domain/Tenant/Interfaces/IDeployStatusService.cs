using Kyoto.Domain.System;

namespace Kyoto.Domain.Tenant.Interfaces;

public interface IDeployStatusService
{
    Task Notify(Session session);
}