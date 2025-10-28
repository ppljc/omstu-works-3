namespace lab4;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Лабораторная работа 4");
            Console.WriteLine("Задачи:");
            Console.WriteLine("1 - Объединение фамилий и имён");
            Console.WriteLine("2 - Пузырьковая сортировка фамилий");
            Console.WriteLine("3 - Очередь на отчисление");
            Console.WriteLine("4 - Англо-русский словарь");
            Console.WriteLine("5 - Список на отчисление (массив + индексатор)");
            Console.WriteLine("6 - Разделение массива на фамилии и имена");
            Console.WriteLine("7 - Сортировка имён вставками");
            Console.WriteLine("8 - Стек мыслей в голове");
            Console.WriteLine("9 - Русско-английский словарь");
            Console.WriteLine("10 - Стек мыслей (массив + индексатор)");
            Console.WriteLine("0 - Выход");
            Console.Write("\nВведите номер задачи: ");

            string input = Console.ReadLine();
            Console.Clear();

            switch (input)
            {
                case "1":
                    Console.WriteLine("Задача 1: Объединение массивов\n");
                    Task1.Execute();
                    break;

                case "2":
                    Console.WriteLine("Задача 2: Пузырьковая сортировка\n");
                    Task2.Execute();
                    break;

                case "3":
                    Console.WriteLine("Задача 3: Очередь на отчисление\n");
                    Task3.Execute();
                    break;

                case "4":
                    Console.WriteLine("Задача 4: Англо-русский словарь\n");
                    Task4.Execute();
                    break;

                case "5":
                    Console.WriteLine("Задача 5: Список на отчисление (массив + индексатор)\n");
                    Task5.Execute();
                    break;

                case "6":
                    Console.WriteLine("Задача 6: Разделение массива\n");
                    Task6.Execute();
                    break;

                case "7":
                    Console.WriteLine("Задача 7: Сортировка имён вставками\n");
                    Task7.Execute();
                    break;

                case "8":
                    Console.WriteLine("Задача 8: Стек мыслей в голове\n");
                    Task8.Execute();
                    break;

                case "9":
                    Console.WriteLine("Задача 9: Русско-английский словарь\n");
                    Task9.Execute();
                    break;

                case "10":
                    Console.WriteLine("Задача 10: Стек мыслей (массив + индексатор)\n");
                    Task10.Execute();
                    break;
                
                case "0":
                    Console.WriteLine("Выход...");
                    return;

                default:
                    Console.WriteLine("Неверный номер задачи.");
                    break;
            }

            Console.WriteLine("\nНажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();
        }
    }
}