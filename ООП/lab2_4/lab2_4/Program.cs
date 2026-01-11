using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_4
{
    public class Program
    {
        static void Main()
        {
            Run();
        }

        public static void Run()
        {
            var keys = new Dictionary<string, string>()
            {
                { "pro123", "ProDocumentWorker" },
                { "exp456", "ExpertDocumentWorker" }
            };

            Console.Write("Введите ключ доступа (или оставьте пустым для бесплатной версии): ");
            string input = Console.ReadLine();
            input = input == null ? string.Empty : input.Trim();

            DocumentWorker worker;

            if (keys.ContainsKey(input) && keys[input] == "ProDocumentWorker")
            {
                worker = new ProDocumentWorker();
                Console.WriteLine("\nАктивирована версия: Pro");
            }
            else if (keys.ContainsKey(input) && keys[input] == "ExpertDocumentWorker")
            {
                worker = new ExpertDocumentWorker();
                Console.WriteLine("\nАктивирована версия: Expert");
            }
            else if (input != "")
            {
                worker = new DocumentWorker();
                Console.WriteLine("\nКлюч не найден. Активирована версия: Free");
            }
            else
            {
                worker = new DocumentWorker();
                Console.WriteLine("\nАктивирована версия: Free");
            }

            Console.WriteLine();
            worker.OpenDocument();
            worker.EditDocument();
            worker.SaveDocument();
        }
    }
}
