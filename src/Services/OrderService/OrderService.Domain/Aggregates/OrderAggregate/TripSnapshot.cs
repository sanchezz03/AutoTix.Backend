namespace OrderService.Domain.Aggregates.OrderAggregate;

public class TripSnapshot
{
    public Guid Id { get; private set; }
    public string StationFrom { get; private set; }
    public string StationTo { get; private set; }

    public List<TripSegmentSnapshot> Segments { get; private set; }

    private TripSnapshot() { }

    public TripSnapshot(string from, string to, List<TripSegmentSnapshot> segments)
    {
        Id = Guid.NewGuid();
        StationFrom = from;
        StationTo = to;
        Segments = segments;
    }
}