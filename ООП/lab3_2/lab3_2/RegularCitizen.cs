namespace lab3_2;

public class RegularCitizen : Client
{
    public RegularCitizen(string name, int experience, double income)
        : base(name, experience, income) { }

    public override double GetCreditAmount()
    {
        double amount = Income * 12;
        if (Experience > 7) amount *= 1.1;
        return amount;
    }

    public override double GetInterestRate()
    {
        double rate = 10;
        if (Income > 100000) rate -= 1.5;
        if (Experience < 2) rate += 1;
        return rate;
    }
}
