namespace lab4;

public class Student : IStudent
{
    private static readonly string[] Surnames =
    [
        "Алексеев", "Ахмедеев", "Бондин", "Вольнов", "Гергерт", "Гончаров", "Жакина", "Зданникова", "Зимин",
        "Кабулов", "Клексин", "Коньков", "Лазарев", "Назарова", "Наурзбаева", "Поварнин", "Решетов", "Сарсембаев",
        "Тастемиров", "Тимошин", "Томский", "Трукан", "Фельдт", "Чернявский", "Чугунов", "Шмидт"
    ];

    private static readonly string[] Names =
    [
        "Никита", "Самир", "Данил", "Кирилл", "Анна", "Пётр", "Динара", "Алина", "Андрей", "Артём", "Роман",
        "Максим", "Матвей", "Анастасия", "Альбина", "Юрий", "Артём", "Георгий", "Рустам", "Андрей", "Михаил",
        "Егор", "Андрей", "Никита", "Данил", "Антон"
    ];

    private static readonly string[] Patronymics =
    [
        "Алексеевич", "Дауленович", "Алексеевич", "Васильевич", "Владимировна", "Валентинович", "Ханатовна",
        "Алексеевна", "Сергеевич", "Жамбылович", "Сергеевич", "Максимович", "Александрович", "Александровна",
        "Хаербековна", "Андреевич", "", "Валерьевич", "Асхатович", "Владимирович", "Евгеньевич", "Викторович",
        "Дмитриевич", "Ваитальевич", "Александрович", "Владиславович"
    ];

    private static readonly Random Random = new();

    public string Surname { get; }
    public string Name { get; }
    public string Patronymic { get; }
    public int Programming { get; }
    public int Philosophy { get; }
    public int Networks { get; }
    public int Optimization { get; }

    private Student(string surname, string name, string patronymic, int programming, int philosophy, int networks,
        int optimization)
    {
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        Programming = programming;
        Philosophy = philosophy;
        Networks = networks;
        Optimization = optimization;
    }

    public static Student GenerateStudent()
    {
        return new Student(
            Surnames[Random.Next(Surnames.Length)],
            Names[Random.Next(Names.Length)],
            Patronymics[Random.Next(Patronymics.Length)],
            Random.Next(2, 6),
            Random.Next(2, 6),
            Random.Next(2, 6),
            Random.Next(2, 6)
        );
    }

    public string GetStudentInfo()
    {
        return $"{Surname} {Name} {Patronymic}\n" +
               $"Программирование: {Programming}\n" +
               $"Философия: {Philosophy}\n" +
               $"Сети: {Networks}\n" +
               $"Методы оптимизации: {Optimization}";
    }

    public bool GetDecision()
    {
        int fails = 0;
        if (Programming < 3) fails++;
        if (Philosophy < 3) fails++;
        if (Networks < 3) fails++;
        if (Optimization < 3) fails++;

        if (fails >= 3) return true;

        if (fails == 2)
        {
            if (Programming < 3 && Networks < 3) return true;
            if (Programming < 3 && Optimization < 3) return true;
            if (Networks < 3 && Optimization < 3) return true;
        }

        return false;
    }
}