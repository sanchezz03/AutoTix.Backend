using Domain.Shared.Entities;

namespace RailwayConnectorService.Domain.Entities;

public class Station : Base<long>
{
    public int ExternalId { get; private set; }
    public string Name { get; private set; }
}
