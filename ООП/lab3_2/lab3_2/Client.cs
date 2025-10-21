namespace lab3_2;

public abstract class Client
{
    public string Name { get; }
    protected int Experience { get; }
    protected double Income { get; }

    protected Client(string name, int experience, double income)
    {
        Name = name;
        Experience = experience;
        Income = income;
    }

    public abstract double GetCreditAmount();
    public abstract double GetInterestRate();
}
