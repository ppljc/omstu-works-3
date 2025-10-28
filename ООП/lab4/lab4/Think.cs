namespace lab4;

public class Think : IThink
{
    private static readonly string[][] ThinkTexts =
    [
        ["Надо сдать лабу", "Зачет по алгоритмам", "Экзамен близко", "Прочитать лекцию"], // Учеба
        ["Не пойти ли мне поесть", "Хочу есть", "Хочу в KFC", "Опять потолстел"], // Еда
        ["Сыграть в Dota", "Новая игра вышла", "Прокачать персонажа", "Выиграть катку"] // Игры
    ];

    private static readonly Random Random = new();

    public ThinkType Type { get; }
    public string Text { get; }

    private Think(ThinkType type, string text)
    {
        Type = type;
        Text = text;
    }

    public static Think GenerateThink()
    {
        ThinkType type = (ThinkType)Random.Next(Enum.GetValues<ThinkType>().Length);
        string[] texts = ThinkTexts[(int)type];
        string text = texts[Random.Next(texts.Length)];
        return new Think(type, text);
    }

    public string GetThinkInfo()
    {
        return $"Тип: {Type}\nМысль: {Text}";
    }

    public bool GetDecision()
    {
        return Type switch
        {
            ThinkType.Учеба => true,
            ThinkType.Еда => !(Text.Contains("KFC") || Text.Contains("потолстел")),
            ThinkType.Игры => false,
            _ => false
        };
    }
}