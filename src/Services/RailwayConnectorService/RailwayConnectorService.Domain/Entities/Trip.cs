using Domain.Shared.Entities;

namespace RailwayConnectorService.Domain.Entities;

public class Trip : Base<long>
{
    public Train Train { get; private set; }
    public Station StationFrom { get; private set; }
    public Station StationTo { get; private set; }
    public DateTime DepartAt { get; private set; }
    public DateTime ArriveAt { get; private set; }
    public List<Wagon> Wagons { get; private set; }
}
