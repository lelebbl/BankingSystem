using BankingSystem.BankingSystem.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.BankingSystem.Core.Commands.AccountManagerCommand;


namespace BankingSystem.BankingSystem.Core.Entities.Accounts
{
    public static class AccountManager
    {
        public static void OpenAccount(List<Account> accounts, TransactionInvoker invoker)
        {
            var command = new OpenAccountCommand(accounts);
            invoker.ExecuteCommand(command);
        }

        public static void DepositToAccount(List<Account> accounts, TransactionInvoker invoker)
        {
            if (accounts.Count == 0)
            {
                Console.WriteLine("У вас нет открытых счетов. Сначала откройте счет.");
                OpenAccount(accounts, invoker);
                return;
            }

            Console.Write("Введите номер счета: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Введите сумму для пополнения: ");
            decimal amount;
            if (decimal.TryParse(Console.ReadLine(), out amount))
            {
                var command = new DepositCommand(accounts, accountNumber, amount);
                invoker.ExecuteCommand(command);
            }
            else
            {
                Console.WriteLine("Некорректная сумма.");
            }
        }

        public static void WithdrawFromAccount(List<Account> accounts, TransactionInvoker invoker)
        {
            if (accounts.Count == 0)
            {
                Console.WriteLine("У вас нет открытых счетов. Сначала откройте счет.");
                OpenAccount(accounts, invoker);
                return;
            }

            Console.Write("Введите номер счета: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Введите сумму для снятия: ");
            decimal amount;
            if (decimal.TryParse(Console.ReadLine(), out amount))
            {
                var command = new WithdrawCommand(accounts, accountNumber, amount);
                invoker.ExecuteCommand(command);
            }
            else
            {
                Console.WriteLine("Некорректная сумма.");
            }
        }

        public static void CloseAccount(List<Account> accounts, TransactionInvoker invoker)
        {
            if (accounts.Count == 0)
            {
                Console.WriteLine("У вас нет открытых счетов. Сначала откройте счет.");
                OpenAccount(accounts, invoker);
                return;
            }

            Console.Write("Введите номер счета: ");
            string accountNumber = Console.ReadLine();

            var command = new CloseAccountCommand(accounts, accountNumber);
            invoker.ExecuteCommand(command);
        }
    }
}
