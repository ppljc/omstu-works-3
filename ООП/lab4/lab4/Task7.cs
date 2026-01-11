using static System.String;

namespace lab4;

public class Task7
{
    public static void Execute()
    {
        string[] names =
        [
            "Никита", "Самир", "Данил", "Кирилл", "Анна", "Пётр", "Динара", "Алина", "Андрей", "Артём", "Роман",
            "Максим", "Матвей", "Анастасия", "Альбина", "Юрий", "Артём", "Георгий", "Рустам", "Андрей", "Михаил",
            "Егор", "Андрей", "Никита", "Данил", "Антон"
        ];

        Console.WriteLine("Исходный массив имён:");
        foreach (var name in names)
        {
            Console.WriteLine(name);
        }

        for (int i = 1; i < names.Length; i++)
        {
            string key = names[i];
            int j = i - 1;

            while (j >= 0 && CompareOrdinal(names[j], key) > 0)
            {
                names[j + 1] = names[j];
                j--;
            }

            names[j + 1] = key;
        }

        Console.WriteLine("\nОтсортированный массив имён (сортировка вставками):");
        foreach (var name in names)
        {
            Console.WriteLine(name);
        }
    }
}