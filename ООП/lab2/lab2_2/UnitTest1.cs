using NUnit.Framework;
using lab2_1;
using System;
using System.IO;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.NetworkInformation;

namespace lab2_2
{
    [TestFixture]
    public class UnitTest1
    {
        private ATM atm;

        [SetUp]
        public void Setup()
        {
            atm = new ATM();
        }

        [TestCase("4321432143214321", "4321")] // debit
        [TestCase("1111222233334444", "1111")] // credit
        [TestCase("1234123412341234", "1234")] // debit + credit
        public void Test_Login(string number, string pin)
        {
            bool result = atm.Login(number, pin);

            Assert.That(result, Is.True);

            Card testCard = atm.CurrentCard;

            Assert.That(testCard, Is.Not.Null);
            Assert.That(testCard.Number, Is.EqualTo(number));
        }

        [TestCase("4321432112341234", "4321")] // incorrect number
        [TestCase("4321432143214321", "1234")] // incorrect pin
        public void Test_Login_Errors(string number, string pin)
        {
            bool result = atm.Login(number, pin);

            Assert.That(result, Is.False);

            Card testCard = atm.CurrentCard;

            Assert.That(testCard, Is.Null);
        }

        [TestCase("debit", typeof(DebitAccount))]
        [TestCase("credit", typeof(CreditAccount))]
        public void Test_CreateAccount(string type, Type expectedType)
        {
            atm.Login("1234123412341234", "1234");
            Card testCard = atm.CurrentCard;
            int beforeCount = testCard.Accounts.Count;

            testCard.CreateAccount(type);

            Assert.That(testCard.Accounts, Has.Count.EqualTo(beforeCount + 1));
            Assert.That(testCard.Accounts.Last(), Is.InstanceOf(expectedType));
        }

        [TestCase(1, typeof(CreditAccount))]
        [TestCase(2, typeof(CurrentAccount))]
        public void Test_SetActiveAccount(int newId, Type expectedType)
        {
            atm.Login("1234123412341234", "1234");
            Card testCard = atm.CurrentCard;

            testCard.SetActiveAccount(newId);

            Assert.That(atm.CurrentCard.ActiveAccount, Is.InstanceOf(expectedType));
        }

        [Test]
        public void Test_SelectActiveAccount_OnlyOne()
        {
            atm.Login("1111222233334444", "1111");
            Card testCard = atm.CurrentCard;

            Assert.Throws<ArgumentOutOfRangeException>(() => testCard.SetActiveAccount(1));
        }

        [Test]
        public void Test_CheckCredit()
        {
            atm.Login("1234123412341234", "1234");
            Card testCard = atm.CurrentCard;

            testCard.SetActiveAccount(1);
            testCard.ActiveAccount.Withdraw(15001);

            testCard.SetActiveAccount(1);
            Assert.Throws<InvalidOperationException>(() => testCard.ActiveAccount.Withdraw(15001));
        }

        [TestCase("4321432143214321", "4321")] // debit
        [TestCase("1111222233334444", "1111")] // credit
        [TestCase("1234123412341234", "1234")] // debit + credit + current (not active)
        [TestCase("1234123411112222", "1122")] // current (no debit)
        [TestCase("1234123433334444", "3344")] // credit + current (not active + no debit)
        public void Test_Deposit_NoBonus(string number, string pin)
        {
            atm.Login(number, pin);
            Card testCard = atm.CurrentCard;

            decimal beforeBalance = testCard.TotalBalance;

            atm.Deposit(1_000_000m);

            Assert.That(testCard.TotalBalance, Is.EqualTo(beforeBalance + 1_000_000));
        }

        [Test]
        public void Test_Deposit_Bonus()
        {
            atm.Login("1234123412341234", "1234");
            Card testCard = atm.CurrentCard;

            decimal beforeBalance = testCard.TotalBalance;

            testCard.SetActiveAccount(2);

            atm.Deposit(1_000_000m);

            Assert.That(testCard.TotalBalance, Is.EqualTo(beforeBalance + 1_000_000 + 2000));
        }

        [Test]
        public void Test_Withdraw()
        {
            atm.Login("4321432143214321", "4321");
            Card testCard = atm.CurrentCard;

            decimal beforeBalance = testCard.TotalBalance;

            atm.Withdraw(500m);

            Assert.That(testCard.TotalBalance, Is.EqualTo(beforeBalance - 500));
        }

        [TestCase(30001, typeof(InvalidOperationException))] // превышение лимита
        [TestCase(-5000, typeof(ArgumentException))]         // отрицательная сумма
        [TestCase(15001, typeof(InvalidOperationException))] // уход в минус
        public void Test_Withdraw_Errors(decimal amount, Type expectedException)
        {
            atm.Login("4321432143214321", "4321");
            Card testCard = atm.CurrentCard;

            Assert.Throws(expectedException, () => atm.Withdraw(amount));
        }


        [Test]
        public void Test_Transfer()
        {
            atm.Login("1234123412341234", "1234");
            Card testCard = atm.CurrentCard;

            decimal fromBefore = testCard.Accounts[0].Balance;
            decimal toBefore = testCard.Accounts[1].Balance;

            atm.Transfer(1, 5000m);

            Assert.That(testCard.Accounts[0].Balance, Is.EqualTo(fromBefore - 5000));
            Assert.That(testCard.Accounts[1].Balance, Is.EqualTo(toBefore + 5000));
        }

        [Test]
        public void Test_Transfer_NotExsistingAccount()
        {
            atm.Login("1234123412341234", "1234");
            Card testCard = atm.CurrentCard;

            Assert.Throws<ArgumentOutOfRangeException>(() => atm.Transfer(-1, 5000m));
        }

        [Test]
        public void Test_TotalBalance()
        {
            atm.Login("1234123412341234", "1234");
            Card testCard = atm.CurrentCard;

            decimal total = testCard.Accounts.Sum(a => a.Balance);

            Assert.That(total, Is.EqualTo(10000 + (-5000)));
        }
    }
}
