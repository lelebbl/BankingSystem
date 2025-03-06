using BankingSystem.BankingSystem.Core.Entities.Operations;
using BankingSystem.BankingSystem.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Entities.Accounts
{
    public abstract class Account
    {
        public string AccountNumber { get; protected set; }
        public decimal Balance { get; protected set; }

        protected Account(string accountNumber, decimal balance)
        {
            AccountNumber = accountNumber;
            Balance = balance;
        }

        public abstract void Deposit(decimal amount);
        public abstract void Withdraw(decimal amount);

        public static Account FindAccount(List<Account> accounts, string accountNumber)
        {
            return accounts.Find(a => a.AccountNumber == accountNumber);
        }

        public static void DisplayAccounts(Client client)
        {
            foreach (var account in client.accounts)
            {
                Console.WriteLine($"Счет №: {account.AccountNumber}");
                Console.WriteLine($"Сумма на счете: {account.Balance} руб.");

                decimal totalLoanAmount = 0;
                decimal totalInstallmentAmount = 0;
                decimal totalRepaymentAmount = 0;
                decimal totalSentAmount = 0;
                decimal totalReceivedAmount = 0;
                decimal depositAmount = 0;
                decimal depositInterestRate = 0;

                foreach (var application in client.applications)
                {
                    if (application.IsApproved && application.Applicant == client && application.AccountNumber == account.AccountNumber)
                    {
                        decimal amountWithInterest = application.Amount + (application.Amount * application.InterestRate / 100);
                        totalRepaymentAmount += amountWithInterest;
                        if (application is Loan)
                        {
                            totalLoanAmount += application.Amount;
                        }
                        else if (application is Installment)
                        {
                            totalInstallmentAmount += application.Amount;
                        }
                    }
                }

                foreach (var transaction in client.transactions)
                {
                    if (transaction.FromAccount == account.AccountNumber)
                    {
                        totalSentAmount += transaction.Amount;
                    }
                    if (transaction.ToAccount == account.AccountNumber)
                    {
                        totalReceivedAmount += transaction.Amount;
                    }
                }

                if (account is DepositAccount depositAccount)
                {
                    depositAmount = depositAccount.DepositAmount;
                    depositInterestRate = depositAccount.DepositInterestRate;
                }

                Console.WriteLine($"Взято в кредит: {totalLoanAmount} руб.");
                Console.WriteLine($"Взято в рассрочку: {totalInstallmentAmount} руб.");
                Console.WriteLine($"Сумма к выплате с учетом процентов: {totalRepaymentAmount} руб.");
                Console.WriteLine($"Отправлено средств: {totalSentAmount} руб.");
                Console.WriteLine($"Получено средств: {totalReceivedAmount} руб.");
                Console.WriteLine($"Сумма по вкладу: {depositAmount} руб.");
                Console.WriteLine($"Процент по вкладу: {depositInterestRate}%");

                Console.WriteLine("----------------------------");
            }
        }
    }
}
