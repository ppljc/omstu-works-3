using static System.String;

namespace lab4;

public class Task2
{
    public static void Execute()
    {
        string[] surnames =
        [
            "Вольнов", "Алексеев", "Ахмедеев", "Бондин", "Гергерт", "Гончаров", "Жакина", "Зданникова", "Зимин",
            "Кабулов", "Клексин", "Коньков", "Лазарев", "Назарова", "Наурзбаева", "Поварнин", "Решетов", "Сарсембаев",
            "Тастемиров", "Тимошин", "Томский", "Трукан", "Фельдт", "Чернявский", "Чугунов", "Шмидт"
        ];

        for (int i = 0; i < surnames.Length - 1; i++)
        {
            var swapped = false;
            for (int j = 0; j < surnames.Length - i - 1; j++)
            {
                if (CompareOrdinal(surnames[j], surnames[j + 1]) > 0)
                {
                    (surnames[j], surnames[j + 1]) = (surnames[j + 1], surnames[j]);
                    swapped = true;
                }
            }

            if (!swapped)
            {
                break;
            }
        }

        Console.WriteLine("Отсортированный массив фамилий:");
        foreach (var t in surnames)
        {
            Console.WriteLine(t);
        }
    }
}