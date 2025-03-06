using BankingSystem.BankingSystem.Core.Entities.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Commands.AccountManagerCommand
{
    public class WithdrawCommand : ICommand
    {
        private List<Account> _accounts;
        private string _accountNumber;
        private decimal _amount;

        public WithdrawCommand(List<Account> accounts, string accountNumber, decimal amount)
        {
            _accounts = accounts;
            _accountNumber = accountNumber;
            _amount = amount;
        }

        public void Execute()
        {
            var account = Account.FindAccount(_accounts, _accountNumber);
            if (account != null)
            {
                if (account.Balance >= _amount)
                {
                    account.Withdraw(_amount);
                    Console.WriteLine($"Снято {_amount}. Текущий баланс: {account.Balance}");
                }
                else
                {
                    Console.WriteLine("Недостаточно средств.");
                }
            }
            else
            {
                Console.WriteLine("Счет не найден.");
            }
        }

        public void Undo()
        {
            var account = Account.FindAccount(_accounts, _accountNumber);
            if (account != null)
            {
                account.Deposit(_amount);
                Console.WriteLine($"Отмена снятия на {_amount}. Текущий баланс: {account.Balance}");
            }
        }
    }
}
