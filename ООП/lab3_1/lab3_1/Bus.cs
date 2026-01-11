namespace lab3_1;

public class Bus : Transport
{
    public int Passengers { get; set; }
    public int DiscountedPassengers { get; set; }
    public double TicketPrice { get; set; }

    public override double CalculateRevenue()
    {
        double regular = (Passengers - DiscountedPassengers) * TicketPrice;
        double discounted = DiscountedPassengers * TicketPrice * 0.5;
        return regular + discounted;
    }
}