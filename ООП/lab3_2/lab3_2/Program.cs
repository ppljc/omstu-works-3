namespace lab3_2;

public class Program
{
    static void Main()
    {
        Client[] clients =
        {
            new BankEmployee("Сотрудник банка", 8, 150000),
            new RegularCitizen("Обычный гражданин", 5, 90000),
            new BadHistoryClient("С плохой историей", 3, 60000, 2),
            new PrivilegedClient("Льготник", 10, 70000, 65)
        };

        foreach (var client in clients)
        {
            double amount = client.GetCreditAmount();
            double rate = client.GetInterestRate();
            Console.WriteLine($"{client.Name}:");
            Console.WriteLine($"Возможный кредит: {amount:F0} руб.");
            Console.WriteLine($"Процентная ставка: {rate:F1}%");
            Console.WriteLine();
        }
    }
}
