namespace lab4;

public class Task5
{
    public static void Execute()
    {
        ExpulsionList expulsionList = new();

        Console.WriteLine("Генерация 10 студентов...\n");

        for (int i = 0; i < 10; i++)
        {
            var student = Student.GenerateStudent();
            Console.WriteLine(
                $"Студент {i + 1}:\n{student.GetStudentInfo()}\nРешение: {(student.GetDecision() ? "ОТЧИСЛЕН" : "ОСТАЁТСЯ")}\n");
            if (student.GetDecision())
            {
                expulsionList.Add(student);
            }
        }

        Console.WriteLine($"В списке на отчисление: {expulsionList.Count}\n");

        Console.WriteLine("Студенты, подлежащие отчислению (через индексатор):");
        for (int i = 0; i < expulsionList.Count; i++)
        {
            var student = expulsionList[i];
            Console.WriteLine($"{student.Surname} {student.Name[0]}. {student.Patronymic[0]}.");
        }
    }
}