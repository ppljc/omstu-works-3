namespace lab3_1.tests;
using lab3_1;
using NUnit.Framework;

[TestFixture]
public class TransportTests
{
    [TestCase(10, 0, 50, 500)]
    [TestCase(10, 2, 50, 450)]
    [TestCase(5, 5, 40, 100)]
    [TestCase(0, 0, 50, 0)]
    public void Test_Bus(int passengers, int discounted, double price, double expected)
    {
        var bus = new Bus { Passengers = passengers, DiscountedPassengers = discounted, TicketPrice = price };
        Assert.That(bus.CalculateRevenue(), Is.EqualTo(expected));
    }

    [TestCase(10, 20, 200)]
    [TestCase(0, 20, 0)]
    [TestCase(5, 15, 75)]
    public void Test_Taxi(double distanceKm, double pricePerKm, double expected)
    {
        var taxi = new Taxi { DistanceKm = distanceKm, PricePerKm = pricePerKm };
        Assert.That(taxi.CalculateRevenue(), Is.EqualTo(expected));
    }

    [TestCase(50, 100, 10, 5500)]
    [TestCase(0, 100, 10, 0)]
    [TestCase(1, 200, 50, 250)]
    [TestCase(5, 100, 10, 550)]
    public void Test_Train(int passengers, double ticketPrice, double serviceFee, double expected)
    {
        var train = new Train { Passengers = passengers, TicketPrice = ticketPrice, ServiceFee = serviceFee };
        Assert.That(train.CalculateRevenue(), Is.EqualTo(expected));
    }

    [Test]
    public void Test_Mixed()
    {
        var bus = new Bus { Passengers = 10, DiscountedPassengers = 2, TicketPrice = 50 };
        var taxi = new Taxi { DistanceKm = 10, PricePerKm = 20 };
        var train = new Train { Passengers = 5, TicketPrice = 100, ServiceFee = 10 };
        var total = bus.CalculateRevenue() + taxi.CalculateRevenue() + train.CalculateRevenue();
        Assert.That(total, Is.EqualTo(1200));
    }
}