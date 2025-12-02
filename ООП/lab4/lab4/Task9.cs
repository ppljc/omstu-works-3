namespace lab4;

public class Task9
{
    public static void Execute()
    {
        Dictionary<string, string> dictionary = new()
        {
            ["привет"] = "hello",
            ["мир"] = "world",
            ["компьютер"] = "computer",
            ["программа"] = "program",
            ["язык"] = "language",
            ["студент"] = "student",
            ["университет"] = "university",
            ["код"] = "code",
            ["алгоритм"] = "algorithm",
            ["данные"] = "data",
            ["сеть"] = "network",
            ["оптимизация"] = "optimization",
            ["мысли"] = "thoughts",
            ["еда"] = "food",
            ["игра"] = "game"
        };

        Console.WriteLine("Русско-английский словарь:");

        foreach (var pair in dictionary)
        {
            Console.WriteLine($"{pair.Key} — {pair.Value}");
        }
    }
}