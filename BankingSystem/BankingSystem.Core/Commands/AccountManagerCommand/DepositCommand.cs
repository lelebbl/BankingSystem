using BankingSystem.BankingSystem.Core.Entities.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Commands.AccountManagerCommand
{
    public class DepositCommand : ICommand
    {
        private List<Account> _accounts;
        private string _accountNumber;
        private decimal _amount;

        public DepositCommand(List<Account> accounts, string accountNumber, decimal amount)
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
                account.Deposit(_amount);
                Console.WriteLine($"Счет пополнен на {_amount}. Текущий баланс: {account.Balance}");
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
                account.Withdraw(_amount);
                Console.WriteLine($"Отмена пополнения на {_amount}. Текущий баланс: {account.Balance}");
            }
        }
    }
}
