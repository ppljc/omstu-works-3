namespace lab4;

public class Task8
{
    public static void Execute()
    {
        Stack<Think> mind = new();

        Console.WriteLine("Симуляция мыслей в голове (10 мыслей)...\n");

        for (int i = 0; i < 10; i++)
        {
            var thought = Think.GenerateThink();
            mind.Push(thought);
            Console.WriteLine($"Мысль {i + 1}:\n{thought.GetThinkInfo()}\nРешение: {(thought.GetDecision() ? "ХОРОШАЯ" : "ПЛОХАЯ")}\n");
        }

        Console.WriteLine($"Всего мыслей в голове: {mind.Count}\n");

        Console.WriteLine("Последние мысли (из стека):");
        while (mind.Count > 0)
        {
            var thought = mind.Pop();
            Console.WriteLine($"- {thought.Text} ({(thought.GetDecision() ? "хорошая" : "плохая")})");
        }
    }
}