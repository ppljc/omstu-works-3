namespace lab5;

public class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Лабораторная работа 5");
            Console.WriteLine("Задачи:");
            Console.WriteLine("1 - Файл списка группы");
            Console.WriteLine("2 - Сканер изображений на дисках");
            Console.WriteLine("3 - Резервное копирование папки");
            Console.WriteLine("0 - Выход");
            Console.Write("\nВведите номер задачи: ");

            string input = Console.ReadLine();
            Console.Clear();

            switch (input)
            {
                case "1":
                    Console.WriteLine("Задача 1: Файл списка группы\n");
                    Task1 task1 = new Task1();
                    task1.Execute();
                    break;

                case "2":
                    Console.WriteLine("Задача 2: Сканер изображений на дисках\n");
                    Task2 task2 = new Task2();
                    task2.Execute();
                    break;
                
                case "3":
                    Console.WriteLine("Задача 3: Резервное копирование папки\n");
                    Task3 task3 = new Task3();
                    task3.Execute();
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