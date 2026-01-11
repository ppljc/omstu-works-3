namespace lab3_2;

public class PrivilegedClient : Client
{
    private int Age;

    public PrivilegedClient(string name, int experience, double income, int age)
        : base(name, experience, income)
    {
        Age = age;
    }

    public override double GetCreditAmount()
    {
        double baseAmount = Income * 10;
        if (Age > 60) baseAmount *= 0.8;
        return baseAmount;
    }

    public override double GetInterestRate()
    {
        double rate = 7;
        if (Age > 60) rate -= 1.5;
        if (Experience > 10) rate -= 0.5;
        return rate < 4 ? 4 : rate;
    }
}
