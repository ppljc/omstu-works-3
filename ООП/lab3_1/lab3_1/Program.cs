using System;
using System.Collections.Generic;

namespace lab3_1;

public class Program
{
    static void Main()
    {
        List<Transport> transports = new List<Transport>
        {
            new Bus { Name = "Автобус №1", Passengers = 40, DiscountedPassengers = 10, TicketPrice = 50 },
            new Taxi { Name = "Такси №2", DistanceKm = 25, PricePerKm = 20 },
            new Train { Name = "Электричка №3", Passengers = 100, TicketPrice = 100, ServiceFee = 10 }
        };

        double totalRevenue = 0;
        foreach (var t in transports)
        {
            double revenue = t.CalculateRevenue();
            Console.WriteLine($"{t.Name}: выручка {revenue} руб.");
            totalRevenue += revenue;
        }

        Console.WriteLine($"\nОбщая выручка за рейс: {totalRevenue} руб.");
    }
}
