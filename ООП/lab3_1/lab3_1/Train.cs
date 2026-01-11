namespace lab3_1;

public class Train : Transport
{
    public int Passengers { get; set; }
    public double TicketPrice { get; set; }
    public double ServiceFee { get; set; }

    public override double CalculateRevenue()
    {
        return Passengers * (TicketPrice + ServiceFee);
    }
}