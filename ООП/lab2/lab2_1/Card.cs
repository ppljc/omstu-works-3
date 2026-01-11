using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_1
{
    public class Card
    {
        public string Number { get; }
        public string Pin { get; }
        public List<Account> Accounts { get; }

        public Card(string number, string pin, List<Account> accounts)
        {
            Number = number;
            Pin = pin;
            Accounts = accounts;
        }

        public Account ActiveAccount => Accounts[0];
        public decimal TotalBalance => Accounts.Sum(a => a.Balance);

        public void CreateAccount(string type)
        {
            int newId = Accounts.Count;
            if (type.ToLower() == "debit")
                Accounts.Add(new DebitAccount(newId));
            else if (type.ToLower() == "credit")
                Accounts.Add(new CreditAccount(newId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void SetActiveAccount(int id)
        {
            if (id >= 0 && id < Accounts.Count)
            {
                var selected = Accounts[id];
                Accounts.RemoveAt(id);
                Accounts.Insert(0, selected);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Счёт по ID не найден.");
            }
        }

        public void PrintCardInfo()
        {
            Console.WriteLine($"Ваша карта: {Number}");
            Console.WriteLine($"Активный счёт: {Accounts[0]}");
            Console.WriteLine("Все счета:");
            for (int i = 0; i < Accounts.Count; i++)
                Console.WriteLine(Accounts[i]);
            Console.WriteLine($"Сумма всех счетов: {TotalBalance}");
        }
    }
}
