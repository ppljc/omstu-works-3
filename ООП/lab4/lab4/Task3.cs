namespace lab4;

public class Task3
{
    public static void Execute()
    {
        Queue<Student> expulsionQueue = new();

        Console.WriteLine("Генерация 10 студентов...\n");

        for (int i = 0; i < 10; i++)
        {
            var student = Student.GenerateStudent();
            Console.WriteLine(
                $"Студент {i + 1}:\n{student.GetStudentInfo()}\nРешение: {(student.GetDecision() ? "ОТЧИСЛЕН" : "ОСТАЁТСЯ")}\n");
            if (student.GetDecision())
            {
                expulsionQueue.Enqueue(student);
            }
        }

        Console.WriteLine($"В очереди на отчисление: {expulsionQueue.Count}\n");

        Console.WriteLine("Студенты, подлежащие отчислению:");
        while (expulsionQueue.Count > 0)
        {
            var student = expulsionQueue.Dequeue();
            Console.WriteLine($"{student.Surname} {student.Name[0]}. {student.Patronymic[0]}.");
        }
    }
}