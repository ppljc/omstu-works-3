namespace lab5;

public class Task1
{
    public void Execute()
    {
        string[] students =
        {
            "Алексеев Никита",
            "Ахмедеев Самир",
            "Бондин Данил",
            "Вольнов Кирилл",
            "Гергерт Анна",
            "Гончаров Пётр",
            "Жакина Динара",
            "Зданникова Алина",
            "Зимин Андрей",
            "Кабулов Артём",
            "Клексин Роман",
            "Коньков Максим",
            "Лазарев Матвей",
            "Назарова Анастасия",
            "Наурзбаева Альбина",
            "Поварнин Юрий",
            "Решетов Артём",
            "Сарсембаев Георгий",
            "Тастемиров Рустам",
            "Тимошин Андрей",
            "Томский Михаил",
            "Трукан Егор",
            "Фельдт Андрей",
            "Чернявский Никита",
            "Чугунов Данил",
            "Шмидт Антон"
        };

        var file = new FileInfo("ИВТ-244.txt");

        using (var writer = new StreamWriter(file.OpenWrite()))
        {
            foreach (var s in students)
                writer.WriteLine(s);
        }

        var backup = new FileInfo("ИВТ-244_backup.txt");
        file.CopyTo(backup.FullName, overwrite: true);

        file.Delete();
    }
}