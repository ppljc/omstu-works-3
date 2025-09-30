using System;
using System.Collections.Generic;
using System.Linq;

namespace lab2_1
{
    public class ATM
    {
        private List<Card> cards = new List<Card>();
        public Card CurrentCard { get; private set; }
        private decimal withdrawnThisSession = 0;

        public ATM()
        {
            cards.Add(
                new Card(
                    "1234123412341234",
                    "1234",
                    new List<Account> {
                        new DebitAccount(0, 10000),
                        new CreditAccount(1, -5000)
                    }
                )
            );
            cards.Add(
                new Card(
                    "1111222233334444",
                    "1111",
                    new List<Account> {
                        new CreditAccount(0, -5000)
                    }
                )
            );
        }

        #region Функциональные методы (для тестов)
        public bool Login(string cardNumber, string pin)
        {
            var card = cards.FirstOrDefault(c => c.Number == cardNumber);
            if (card != null && card.Pin == pin)
            {
                CurrentCard = card;
                withdrawnThisSession = 0;
                return true;
            }
            return false;
        }

        public void CreateAccount(string type)
        {
            int newId = CurrentCard.Accounts.Count;
            if (type.ToLower() == "debit")
                CurrentCard.Accounts.Add(new DebitAccount(newId));
            else if (type.ToLower() == "credit")
                CurrentCard.Accounts.Add(new CreditAccount(newId));
        }

        public void SelectActiveAccount(int id)
        {
            if (id >= 0 && id < CurrentCard.Accounts.Count)
            {
                var selected = CurrentCard.Accounts[id];
                CurrentCard.Accounts.RemoveAt(id);
                CurrentCard.Accounts.Insert(0, selected);
            }
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма пополнения должна быть положительной.");

            CurrentCard.ActiveAccount.Deposit(amount);

            if (amount > 1_000_000)
            {
                var debit = CurrentCard.Accounts.FirstOrDefault(a => a is DebitAccount);
                if (debit != null)
                    debit.Deposit(2000);
            }
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма снятия должна быть положительной.");

            if (withdrawnThisSession + amount > 30000)
                throw new InvalidOperationException("Нельзя снять больше 30000 за сеанс.");

            CurrentCard.ActiveAccount.Withdraw(amount);
            withdrawnThisSession += amount;
        }

        public void Transfer(int targetId, decimal amount)
        {
            if (targetId <= 0 || targetId >= CurrentCard.Accounts.Count)
                throw new ArgumentOutOfRangeException("Неверный ID счёта для перевода.");

            CurrentCard.ActiveAccount.Withdraw(amount);
            CurrentCard.Accounts[targetId].Deposit(amount);
        }

        public Card GetCard(string cardNumber) => cards.FirstOrDefault(c => c.Number == cardNumber);
        public List<Card> GetAllCards() => cards;

        public string GetCardInfo()
        {
            if (CurrentCard == null)
                return "Нет выбранной карты.";

            var info = $"Ваша карта: {CurrentCard.Number}\n" +
                       $"Активный счёт: {CurrentCard.ActiveAccount}\n" +
                       "Все счета:\n";

            for (int i = 0; i < CurrentCard.Accounts.Count; i++)
                info += $"ID {i}, {CurrentCard.Accounts[i]}, баланс: {CurrentCard.Accounts[i].Balance}\n";

            info += $"Сумма всех счетов: {CurrentCard.Accounts.Sum(a => a.Balance)}";

            return info;
        }
        #endregion

        #region Меню и интерактивный ввод
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Главное меню ===");
                Console.WriteLine("1. Новый сеанс");
                Console.WriteLine("2. Выйти");
                Console.Write("Ввод: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                    StartSession();
                else if (choice == "2")
                    break;
            }
        }

        private void StartSession()
        {
            Console.Clear();
            Console.Write("Введите номер карты (16 символов): ");
            string number = Console.ReadLine();

            Card card = cards.FirstOrDefault(c => c.Number == number);
            if (card == null)
            {
                Console.WriteLine("Такой карты не существует.");
                Console.ReadKey();
                return;
            }

            int attempts = 3;
            while (attempts > 0)
            {
                Console.Write("Введите PIN (4 символа): ");
                string pin = Console.ReadLine();

                if (card.Pin == pin)
                {
                    CurrentCard = card;
                    withdrawnThisSession = 0;
                    SessionMenu();
                    return;
                }
                else
                {
                    attempts--;
                    Console.WriteLine($"Неверный PIN. Осталось попыток: {attempts}");
                    if (attempts == 0)
                    {
                        Console.WriteLine("Ваша карта заблокирована, 40 волков уже выехало...");
                        Environment.Exit(0);
                    }
                }
            }
        }

        private void SessionMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(GetCardInfo());
                Console.WriteLine("\n=== Меню сеанса ===");
                Console.WriteLine("1. Создать новый счёт");

                if (CurrentCard.Accounts.Count > 1)
                {
                    Console.WriteLine("2. Выбрать активный счёт");
                    Console.WriteLine("3. Перевод");
                    Console.WriteLine("4. Пополнить");
                    Console.WriteLine("5. Снять");
                    Console.WriteLine("6. Выйти в главное меню");
                }
                else
                {
                    Console.WriteLine("2. Пополнить");
                    Console.WriteLine("3. Снять");
                    Console.WriteLine("4. Выйти в главное меню");
                }

                Console.Write("Ввод: ");
                string choice = Console.ReadLine();

                try
                {
                    if (CurrentCard.Accounts.Count > 1)
                    {
                        switch (choice)
                        {
                            case "1": MenuCreateAccount(); break;
                            case "2": MenuSelectActiveAccount(); break;
                            case "3": MenuTransfer(); break;
                            case "4": MenuDeposit(); break;
                            case "5": MenuWithdraw(); break;
                            case "6": return;
                        }
                    }
                    else
                    {
                        switch (choice)
                        {
                            case "1": MenuCreateAccount(); break;
                            case "2": MenuDeposit(); break;
                            case "3": MenuWithdraw(); break;
                            case "4": return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка: " + ex.Message);
                    Console.WriteLine("Нажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                }
            }
        }

        #region Методы меню
        private void MenuCreateAccount()
        {
            Console.WriteLine("Выберите тип счёта:");
            Console.WriteLine("1. Дебетовый");
            Console.WriteLine("2. Кредитный");
            string choice = Console.ReadLine();
            if (choice == "1") CreateAccount("debit");
            else if (choice == "2") CreateAccount("credit");
        }

        private void MenuSelectActiveAccount()
        {
            Console.WriteLine("Выберите счёт по ID:");
            for (int i = 0; i < CurrentCard.Accounts.Count; i++)
                Console.WriteLine($"{i}. {CurrentCard.Accounts[i]}");
            int id = int.Parse(Console.ReadLine());
            SelectActiveAccount(id);
        }

        private void MenuDeposit()
        {
            Console.Write("Введите сумму: ");
            decimal amount = decimal.Parse(Console.ReadLine());
            Deposit(amount);
        }

        private void MenuWithdraw()
        {
            Console.Write("Введите сумму: ");
            decimal amount = decimal.Parse(Console.ReadLine());
            Withdraw(amount);
        }

        private void MenuTransfer()
        {
            Console.WriteLine("Введите ID счёта для перевода:");
            for (int i = 0; i < CurrentCard.Accounts.Count; i++)
                Console.WriteLine($"{i}. {CurrentCard.Accounts[i]}");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Введите сумму: ");
            decimal amount = decimal.Parse(Console.ReadLine());
            Transfer(id, amount);
        }
        #endregion

        #endregion
    }
}
