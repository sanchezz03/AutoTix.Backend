namespace OrderService.Domain.Aggregates.OrderAggregate;

public class TicketReservation
{
    public Guid Id { get; private set; }
    public int Quantity { get; private set; }

    public TripSnapshot Trip { get; private set; }

    private TicketReservation() { }

    public TicketReservation(int quantity, TripSnapshot trip)
    {
        Id = Guid.NewGuid();
        Quantity = quantity;
        Trip = trip;
    } 

    public decimal CalculateAmount()
    {
        if (Trip?.Segments == null || Trip.Segments.Count == 0)
            return 0;

        var segment = Trip.Segments.First();

        if (segment.Train?.WagonClasses == null || segment.Train.WagonClasses.Count == 0)
            return 0;

        decimal ticketPrice = segment.Train.WagonClasses.Min(w => w.Price);

        return ticketPrice * Quantity;
    }
}
