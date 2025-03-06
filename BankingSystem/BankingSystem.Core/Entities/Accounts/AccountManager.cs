using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Entities.Accounts
{
    public static class AccountManager
    {
        public static void OpenAccount(List<Account> accounts)
        {
            string accountNumber = Guid.NewGuid().ToString();
            accounts.Add(new CheckingAccount(accountNumber, 0));
            Console.WriteLine($"Счет открыт. Номер счета: {accountNumber}");
        }

        public static void DepositToAccount(List<Account> accounts)
        {
            if (accounts.Count == 0)
            {
                Console.WriteLine("У вас нет открытых счетов. Сначала откройте счет.");
                OpenAccount(accounts);
                return;
            }

            Console.Write("Введите номер счета: ");
            string accountNumber = Console.ReadLine();

            Account account = Account.FindAccount(accounts, accountNumber);
            if (account != null)
            {
                Console.Write("Введите сумму для пополнения: ");
                decimal amount;
                if (decimal.TryParse(Console.ReadLine(), out amount))
                {
                    account.Deposit(amount);
                    Console.WriteLine($"Счет пополнен на {amount}. Текущий баланс: {account.Balance}");
                }
                else
                {
                    Console.WriteLine("Некорректная сумма.");
                }
            }
            else
            {
                Console.WriteLine("Счет не найден.");
            }
        }

        public static void WithdrawFromAccount(List<Account> accounts)
        {
            if (accounts.Count == 0)
            {
                Console.WriteLine("У вас нет открытых счетов. Сначала откройте счет.");
                OpenAccount(accounts);
                return;
            }

            Console.Write("Введите номер счета: ");
            string accountNumber = Console.ReadLine();

            Account account = Account.FindAccount(accounts, accountNumber);
            if (account != null)
            {
                Console.Write("Введите сумму для снятия: ");
                decimal amount;
                if (decimal.TryParse(Console.ReadLine(), out amount))
                {
                    if (account.Balance >= amount)
                    {
                        account.Withdraw(amount);
                        Console.WriteLine($"Снято {amount}. Текущий баланс: {account.Balance}");
                    }
                    else
                    {
                        Console.WriteLine("Недостаточно средств.");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректная сумма.");
                }
            }
            else
            {
                Console.WriteLine("Счет не найден.");
            }
        }

        public static void CloseAccount(List<Account> accounts)
        {
            if (accounts.Count == 0)
            {
                Console.WriteLine("У вас нет открытых счетов. Сначала откройте счет.");
                OpenAccount(accounts);
                return;
            }

            Console.Write("Введите номер счета: ");
            string accountNumber = Console.ReadLine();

            Account account = Account.FindAccount(accounts, accountNumber);
            if (account != null)
            {
                accounts.Remove(account);
                Console.WriteLine("Счет закрыт.");
            }
            else
            {
                Console.WriteLine("Счет не найден.");
            }
        }
    }
}
