namespace lab4;

public class Task6
{
    public static void Execute()
    {
        string[] mixed =
        [
            "Алексеев", "Никита",
            "Ахмедеев", "Самир",
            "Бондин", "Данил",
            "Вольнов", "Кирилл",
            "Гергерт", "Анна",
            "Гончаров", "Пётр",
            "Жакина", "Динара",
            "Зданникова", "Алина",
            "Зимин", "Андрей",
            "Кабулов", "Артём",
            "Клексин", "Роман",
            "Коньков", "Максим",
            "Лазарев", "Матвей",
            "Назарова", "Анастасия",
            "Наурзбаева", "Альбина",
            "Поварнин", "Юрий",
            "Решетов", "Артём",
            "Сарсембаев", "Георгий",
            "Тастемиров", "Рустам",
            "Тимошин", "Андрей",
            "Томский", "Михаил",
            "Трукан", "Егор",
            "Фельдт", "Андрей",
            "Чернявский", "Никита",
            "Чугунов", "Данил",
            "Шмидт", "Антон"
        ];

        string[] surnames = new string[mixed.Length / 2];
        string[] names = new string[mixed.Length / 2];

        for (int i = 0; i < mixed.Length; i += 2)
        {
            surnames[i / 2] = mixed[i];
            names[i / 2] = mixed[i + 1];
        }

        Console.WriteLine("Исходный массив (фамилии по чётным, имена по нечётным индексам):");
        for (int i = 0; i < mixed.Length; i++)
        {
            Console.WriteLine($"[{i}] {mixed[i]}");
        }

        Console.WriteLine("\nФамилии:");
        foreach (var s in surnames)
        {
            Console.WriteLine(s);
        }

        Console.WriteLine("\nИмена:");
        foreach (var n in names)
        {
            Console.WriteLine(n);
        }
    }
}