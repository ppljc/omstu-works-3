namespace lab3_1;

public class Taxi : Transport
{
    public double DistanceKm { get; set; }
    public double PricePerKm { get; set; }

    public override double CalculateRevenue()
    {
        return DistanceKm * PricePerKm;
    }
}