namespace OrderService.Domain.Aggregates.OrderAggregate;

public class WagonClassSnapshot
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int FreeSeats { get; set; }
    public decimal Price { get; set; }
    public List<string> Amenities { get; set; }
}
