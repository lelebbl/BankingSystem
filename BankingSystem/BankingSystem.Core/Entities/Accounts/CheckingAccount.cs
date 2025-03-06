using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Entities.Accounts
{
    public class CheckingAccount : Account
    {
        public CheckingAccount(string accountNumber, decimal balance) : base(accountNumber, balance) { }

        public override void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public override void Withdraw(decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
            }
            else
            {
                Console.WriteLine("Недостаточно средств.");
            }
        }
    }
}
