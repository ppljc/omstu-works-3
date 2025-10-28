namespace lab4;

public class Task4
{
    public static void Execute()
    {
        Dictionary<string, string> dictionary = new()
        {
            ["hello"] = "привет",
            ["world"] = "мир",
            ["computer"] = "компьютер",
            ["program"] = "программа",
            ["language"] = "язык",
            ["student"] = "студент",
            ["university"] = "университет",
            ["code"] = "код",
            ["algorithm"] = "алгоритм",
            ["data"] = "данные"
        };

        Console.WriteLine("Англо-русский словарь:");
        foreach (var pair in dictionary)
        {
            Console.WriteLine($"{pair.Key} - {pair.Value}");
        }
    }
}