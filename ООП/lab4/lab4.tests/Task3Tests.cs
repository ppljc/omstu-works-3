namespace lab4.tests;

using NUnit.Framework;
using lab4;

[TestFixture]
public class Task3Tests
{
    [Test]
    [Repeat(10)]
    public void Test_GetDecision()
    {
        var student = Student.GenerateStudent();

        int fails = 0;
        if (student.Programming < 3) fails++;
        if (student.Philosophy < 3) fails++;
        if (student.Networks < 3) fails++;
        if (student.Optimization < 3) fails++;

        bool expected = fails >= 3 ||
                        (fails == 2 && student is { Programming: < 3, Networks: < 3 } or { Programming: < 3, Optimization: < 3 } or { Networks: < 3, Optimization: < 3 });

        Assert.That(student.GetDecision(), Is.EqualTo(expected));
    }
}