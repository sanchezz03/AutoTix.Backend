using Domain.Shared.Entities;

namespace RailwayConnectorService.Domain.Entities;

public class Train : Base<long>
{
    public string Number { get; private set; }
    public int Type { get; private set; }
}
