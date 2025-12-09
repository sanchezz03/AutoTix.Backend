namespace OrderService.Domain.Aggregates.OrderAggregate;

public class TripSegmentSnapshot
{
    public Guid Id { get; private set; }

    public int SegmentId { get; private set; }
    public long DepartAt { get; private set; }
    public long ArriveAt { get; private set; }
    public string StationFrom { get; private set; }
    public string StationTo { get; private set; }
    public int StationsTimeOffset { get; private set; }

    public TrainSnapshot Train { get; private set; }

    private TripSegmentSnapshot() { }

    public TripSegmentSnapshot(
        int segmentId,
        long departAt,
        long arriveAt,
        string stationFrom,
        string stationTo,
        int offset,
        TrainSnapshot train)
    {
        Id = Guid.NewGuid();
        SegmentId = segmentId;
        DepartAt = departAt;
        ArriveAt = arriveAt;
        StationFrom = stationFrom;
        StationTo = stationTo;
        StationsTimeOffset = offset;
        Train = train;
    }
}
