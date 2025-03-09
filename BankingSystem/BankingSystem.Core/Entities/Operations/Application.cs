using BankingSystem.BankingSystem.Core.Entities.Users;
using BankingSystem.BankingSystem.Core.Entities.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Entities.Operations
{
    public abstract class Application
    {
        public Client Applicant { get; }
        public decimal Amount { get; }
        public int TermMonths { get; }
        public bool IsApproved { get; private set; }
        public decimal InterestRate { get; }
        public string AccountNumber { get; }

        protected Application(Client applicant, decimal amount, int termMonths, string accountNumber)
        {
            Applicant = applicant;
            Amount = amount;
            TermMonths = termMonths;
            IsApproved = false;
            InterestRate = CalculateInterestRate(termMonths);
            AccountNumber = accountNumber;
        }

        public void Approve()
        {
            IsApproved = true;

            var account = Account.FindAccount(Applicant.accounts, AccountNumber);
            if (account != null)
            {
                account.Deposit(Amount);
                Console.WriteLine($"Заявка одобрена. {Amount} руб. зачислены на счет клиента.");
            }
            else
            {
                Console.WriteLine("Ошибка: счет не найден.");
            }
        }

        public void RevokeApproval()
        {
            IsApproved = false;

            var account = Account.FindAccount(Applicant.accounts, AccountNumber);
            if (account != null)
            {
                account.Withdraw(Amount);
                Console.WriteLine($"Одобрение заявки отменено. {Amount} руб. сняты со счета клиента.");
            }
            else
            {
                Console.WriteLine("Ошибка: счет не найден.");
            }
        }

        private decimal CalculateInterestRate(int termMonths)
        {
            if (termMonths <= 3) return 5;
            if (termMonths <= 6) return 10;
            if (termMonths <= 12) return 15;
            if (termMonths <= 24) return 20;
            return 25;
        }

        public abstract string GetApplicationType();
    }
}
