using NUnit.Framework;
using lab2_1; // твой основной проект
using System;
using System.IO;
using System.Linq;

namespace lab2_2
{
    [TestFixture]
    public class UnitTest1
    {
        private ATM atm;
        private Card cardWithDebitAndCredit;
        private Card cardWithOnlyCredit;

        [SetUp]
        public void Setup()
        {
            atm = new ATM();

            bool loggedIn = atm.Login("1234123412341234", "1234");
            Assert.IsTrue(loggedIn);

            testCard = atm.CurrentCard;
        }

        [Test]
        public void Test_CreateDebitAccount_AddsAccount()
        {
            int beforeCount = testCard.Accounts.Count;

            atm.CreateAccount("debit");

            Assert.AreEqual(beforeCount + 1, testCard.Accounts.Count);
            Assert.IsInstanceOf<DebitAccount>(testCard.Accounts.Last());
        }

        [Test]
        public void Test_CreateCreditAccount_AddsAccount()
        {
            int beforeCount = testCard.Accounts.Count;

            atm.CreateAccount("credit");

            Assert.AreEqual(beforeCount + 1, testCard.Accounts.Count);
            Assert.IsInstanceOf<CreditAccount>(testCard.Accounts.Last());
        }

        [Test]
        public void Test_SelectActiveAccount_ChangesActiveAccount()
        {
            atm.SelectActiveAccount(1);
            Assert.AreEqual(testCard.Accounts[0], testCard.ActiveAccount);
        }

        [Test]
        public void Test_Deposit_AddsMoneyAndBonus()
        {
            decimal beforeBalance = testCard.ActiveAccount.Balance;

            atm.Deposit(1_000_001m);

            var debit = testCard.Accounts.First(a => a is DebitAccount);
            Assert.AreEqual(beforeBalance + 1_000_001 + 2000, debit.Balance);
        }

        [Test]
        public void Test_Deposit_SmallAmount_NoBonus()
        {
            decimal beforeBalance = testCard.ActiveAccount.Balance;

            atm.Deposit(500m);

            Assert.AreEqual(beforeBalance + 500, testCard.ActiveAccount.Balance);
        }

        [Test]
        public void Test_Withdraw_DecreasesBalance()
        {
            decimal beforeBalance = testCard.ActiveAccount.Balance;

            atm.Withdraw(500m);

            Assert.AreEqual(beforeBalance - 500, testCard.ActiveAccount.Balance);
        }

        [Test]
        public void Test_Withdraw_OverSessionLimit_Throws()
        {
            atm.Withdraw(30000m);

            Assert.Throws<System.InvalidOperationException>(() => atm.Withdraw(1m));
        }

        [Test]
        public void Test_Transfer_MovesMoneyBetweenAccounts()
        {
            decimal fromBefore = testCard.Accounts[0].Balance;
            decimal toBefore = testCard.Accounts[1].Balance;

            atm.Transfer(1, 200m);

            Assert.AreEqual(fromBefore - 200, testCard.Accounts[0].Balance);
            Assert.AreEqual(toBefore + 200, testCard.Accounts[1].Balance);
        }

        [Test]
        public void Test_TotalBalance_Calculation()
        {
            decimal total = testCard.Accounts.Sum(a => a.Balance);
            Assert.AreEqual(10000 + (-5000), total);
        }

        [Test]
        public void Test_Login_InvalidPin_Fails()
        {
            var atm2 = new ATM();
            bool loggedIn = atm2.Login("1234123412341234", "0000");
            Assert.IsFalse(loggedIn);
        }
    }
}
