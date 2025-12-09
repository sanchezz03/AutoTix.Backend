namespace OrderService.Domain.Aggregates.OrderAggregate;

public class TrainSnapshot
{
    public Guid Id { get; private set; }

    public int TrainId { get; private set; }
    public string StationFrom { get; private set; }
    public string StationTo { get; private set; }
    public string Number { get; private set; }
    public int Type { get; private set; }

    public List<WagonClassSnapshot> WagonClasses { get; private set; }

    public string WagonClassesJson { get; private set; }

    private TrainSnapshot() { }

    public TrainSnapshot(int trainId, string stationFrom, string stationTo, string number, int type, List<WagonClassSnapshot> classes)
    {
        Id = Guid.NewGuid();
        TrainId = trainId;
        StationFrom = stationFrom;
        StationTo = stationTo;
        Number = number;
        Type = type;
        WagonClasses = classes;

        WagonClassesJson = System.Text.Json.JsonSerializer.Serialize(classes);
    }

    public void EnsureWagonClassesDeserialized()
    {
        if ((WagonClasses == null || WagonClasses.Count == 0) && !string.IsNullOrEmpty(WagonClassesJson))
        {
            WagonClasses = System.Text.Json.JsonSerializer.Deserialize<List<WagonClassSnapshot>>(WagonClassesJson)
                           ?? new List<WagonClassSnapshot>();
        }
    }
}