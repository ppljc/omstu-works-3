namespace lab3_2;

public class BankEmployee : Client
{
    public BankEmployee(string name, int experience, double income) 
        : base(name, experience, income) { }

    public override double GetCreditAmount()
    {
        double baseAmount = Income * 20;
        if (Experience > 5) baseAmount *= 1.2;
        return baseAmount;
    }

    public override double GetInterestRate()
    {
        double rate = 5 - Experience * 0.1;
        return rate < 3 ? 3 : rate;
    }
}
