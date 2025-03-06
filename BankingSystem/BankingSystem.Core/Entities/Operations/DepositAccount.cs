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

        public static void CreateDeposit(Client client, TransactionInvoker invoker)
        {
            if (client.accounts.Count == 0)
            {
                Console.WriteLine("У вас нет открытых счетов. Сначала откройте счет.");
                AccountManager.OpenAccount(client.accounts, invoker);
                return;
            }

            Console.Write("Введите номер счета для вклада: ");
            string accountNumber = Console.ReadLine();
            var account = Account.FindAccount(client.accounts, accountNumber);

            if (account != null)
            {
                Console.Write("Введите сумму вклада: ");
                decimal depositAmount = decimal.Parse(Console.ReadLine());

                if (account.Balance >= depositAmount)
                {
                    Console.Write("Введите срок (месяцы): ");
                    int termMonths = int.Parse(Console.ReadLine());

                    decimal interestRate = CalculateInterestRate(termMonths);
                    account.Withdraw(depositAmount);
                    client.accounts.Add(new DepositAccount(accountNumber, account.Balance, depositAmount, interestRate, termMonths));

                    Console.WriteLine($"Вклад создан на сумму {depositAmount} с процентом {interestRate}%. Текущий баланс: {account.Balance}");
                }
                else
                {
                    Console.WriteLine("Недостаточно средств на счете.");
                }
            }
            else
            {
                Console.WriteLine("Счет не найден.");
            }
        }

        private static decimal CalculateInterestRate(int termMonths)
        {
            if (termMonths <= 3) return 5;
            if (termMonths <= 6) return 10;
            if (termMonths <= 12) return 15;
            if (termMonths <= 24) return 20;
            return 25;
        }
    }
}

  
