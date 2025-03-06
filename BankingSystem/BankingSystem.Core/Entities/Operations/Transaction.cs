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
    public class Transaction
    {
        public string FromAccount { get; private set; }
        public string ToAccount { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }

        public Transaction(string fromAccount, string toAccount, decimal amount)
        {
            FromAccount = fromAccount;
            ToAccount = toAccount;
            Amount = amount;
            Date = DateTime.Now;
        }
    }
}
