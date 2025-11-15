using Domain.Shared.Entities;

namespace RailwayConnectorService.Domain.Entities;

public class Wagon : Base<long>
{
    public string ClassId { get; private set; }
    public string Name { get; private set; }
    public int FreeSeats { get; private set; }
    public decimal Price { get; private set; }
    public List<string> Amenities { get; private set; }
}
