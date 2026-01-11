namespace lab3_2;

public class BadHistoryClient : Client
{
    private int MissedPayments;

    public BadHistoryClient(string name, int experience, double income, int missedPayments)
        : base(name, experience, income)
    {
        MissedPayments = missedPayments;
    }

    public override double GetCreditAmount()
    {
        double baseAmount = Income * 6;
        baseAmount -= MissedPayments * 50000;
        if (baseAmount < 100000) baseAmount = 100000;
        return baseAmount;
    }

    public override double GetInterestRate()
    {
        double rate = 18 + MissedPayments * 2;
        if (Experience > 5) rate -= 1;
        return rate;
    }
}
