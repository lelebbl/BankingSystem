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

        public static void CreateTransaction(Client client, TransactionInvoker invoker)
        {
            if (client.accounts.Count == 0)
            {
                Console.WriteLine("У вас нет открытых счетов. Сначала откройте счет.");
                AccountManager.OpenAccount(client.accounts, invoker);
                return;
            }

            Console.Write("Введите номер счета, с которого хотите перевести средства: ");
            string fromAccountNumber = Console.ReadLine();
            var fromAccount = Account.FindAccount(client.accounts, fromAccountNumber);

            if (fromAccount == null)
            {
                Console.WriteLine("Счет не найден.");
                return;
            }

            Console.Write("Введите номер счета, на который хотите перевести средства: ");
            string toAccountNumber = Console.ReadLine();
            var toAccount = Account.FindAccount(client.accounts, toAccountNumber);

            if (toAccount == null)
            {
                Console.WriteLine("Счет не найден.");
                return;
            }

            Console.Write("Введите сумму перевода: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            if (fromAccount.Balance < amount)
            {
                Console.WriteLine("Недостаточно средств.");
                return;
            }

            fromAccount.Withdraw(amount);
            toAccount.Deposit(amount);

            client.transactions.Add(new Transaction(fromAccountNumber, toAccountNumber, amount));
            Console.WriteLine($"Перевод {amount} руб. выполнен успешно с {fromAccountNumber} на {toAccountNumber}. Текущий баланс: {fromAccount.Balance}");
        }
    }
}
