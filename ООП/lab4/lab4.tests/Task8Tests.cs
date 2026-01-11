namespace lab4.tests;
using NUnit.Framework;
using lab4;

[TestFixture]
public class Task8Tests
{
    [Test]
    [Repeat(10)]
    public void GetDecision_ShouldBeCorrectForGeneratedThink()
    {
        var think = Think.GenerateThink();

        bool expected = think.Type switch
        {
            ThinkType.Учеба => true,
            ThinkType.Еда => !(think.Text.Contains("KFC") || think.Text.Contains("потолстел")),
            ThinkType.Игры => false,
            _ => false
        };

        Assert.That(think.GetDecision(), Is.EqualTo(expected));
    }
}