using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Entities.Operations
{
    public abstract class Enterprise
    {
        public string Type { get; }
        public string LegalName { get; }
        public string UNP { get; }
        public string LegalAddress { get; }
        public Bank Bank { get; }
        public string BIK { get; protected set; }
        public List<Account> Accounts { get; protected set; } = new List<Account>();
        public List<string> Employees { get; protected set; } = new List<string>();
        public decimal Balance { get; protected set; } = 0;

        protected Enterprise(string type, string legalName, string unp, string legalAddress, Bank bank, string bik)
        {
            Type = type;
            LegalName = legalName;
            UNP = unp;
            LegalAddress = legalAddress;
            Bank = bank;
            BIK = bik;
        }

        public void AddAccount(Account account)
        {
            Accounts.Add(account);
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
            }
            else
            {
                throw new Exception("Insufficient funds.");
            }
        }
    }
}
