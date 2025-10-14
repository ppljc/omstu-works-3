using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2_1
{
    public sealed class CurrentAccount : Account
    {
        protected override string AccountType => "Текущий счёт";

        public CurrentAccount(int id, decimal balance = 0) : base(id, balance) { }
    }
}
