namespace lab3_2.tests;
using NUnit.Framework;
using lab3_2;

[TestFixture]
public class CreditTests
{
    [TestCase(8, 150000, 3600000)]
    [TestCase(3, 100000, 2000000)]
    public void Test_BankEmployee_CreditAmount(int exp, double income, double expected)
    {
        var client = new BankEmployee("test", exp, income);
        double result = client.GetCreditAmount();
        Assert.That(result, Is.EqualTo(expected).Within(0.01));
    }

    [TestCase(8, 150000, 4.2)]
    [TestCase(30, 100000, 3)]
    public void Test_BankEmployee_InterestRate(int exp, double income, double expected)
    {
        var client = new BankEmployee("test", exp, income);
        double result = client.GetInterestRate();
        Assert.That(result, Is.EqualTo(expected).Within(0.01));
    }

    [TestCase(10, 120000, 1584000)]
    [TestCase(3, 80000, 960000)]
    public void Test_RegularCitizen_CreditAmount(int exp, double income, double expected)
    {
        var client = new RegularCitizen("test", exp, income);
        double result = client.GetCreditAmount();
        Assert.That(result, Is.EqualTo(expected).Within(0.01));
    }

    [TestCase(5, 120000, 8.5)]
    [TestCase(1, 90000, 11)]
    public void Test_RegularCitizen_InterestRate(int exp, double income, double expected)
    {
        var client = new RegularCitizen("test", exp, income);
        double result = client.GetInterestRate();
        Assert.That(result, Is.EqualTo(expected).Within(0.01));
    }

    [TestCase(5, 60000, 1, 310000)]
    [TestCase(8, 60000, 3, 210000)]
    public void Test_BadHistoryClient_CreditAmount(int exp, double income, int missed, double expected)
    {
        var client = new BadHistoryClient("test", exp, income, missed);
        double result = client.GetCreditAmount();
        Assert.That(result, Is.EqualTo(expected).Within(0.01));
    }

    [TestCase(3, 60000, 0, 18)]
    [TestCase(7, 60000, 2, 21)]
    public void Test_BadHistoryClient_InterestRate(int exp, double income, int missed, double expected)
    {
        var client = new BadHistoryClient("test", exp, income, missed);
        double result = client.GetInterestRate();
        Assert.That(result, Is.EqualTo(expected).Within(0.01));
    }

    [TestCase(10, 70000, 65, 560000)]
    [TestCase(8, 70000, 50, 700000)]
    public void Test_PrivilegedClient_CreditAmount(int exp, double income, int age, double expected)
    {
        var client = new PrivilegedClient("test", exp, income, age);
        double result = client.GetCreditAmount();
        Assert.That(result, Is.EqualTo(expected).Within(0.01));
    }

    [TestCase(10, 70000, 65, 5.5)]
    [TestCase(12, 70000, 55, 6.5)]
    [TestCase(8, 70000, 40, 7)]
    public void Test_PrivilegedClient_InterestRate(int exp, double income, int age, double expected)
    {
        var client = new PrivilegedClient("test", exp, income, age);
        double result = client.GetInterestRate();
        Assert.That(result, Is.EqualTo(expected).Within(0.01));
    }
}