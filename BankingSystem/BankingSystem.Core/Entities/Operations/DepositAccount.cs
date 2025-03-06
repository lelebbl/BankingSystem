using BankingSystem.BankingSystem.Core.Commands;
using BankingSystem.BankingSystem.Core.Entities.Accounts;
using BankingSystem.BankingSystem.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Entities.Operations
{
    public class DepositAccount : Account
    {
        public decimal DepositAmount { get; private set; }
        public decimal DepositInterestRate { get; private set; }
        public int TermMonths { get; private set; }

        public DepositAccount(string accountNumber, decimal balance, decimal depositAmount, decimal depositInterestRate, int termMonths)
            : base(accountNumber, balance)
        {
            DepositAmount = depositAmount;
            DepositInterestRate = depositInterestRate;
            TermMonths = termMonths;
        }

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

  
