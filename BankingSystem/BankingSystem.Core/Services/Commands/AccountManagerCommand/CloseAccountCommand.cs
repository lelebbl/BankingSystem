using BankingSystem.BankingSystem.Core.Entities.Operations;
using BankingSystem.BankingSystem.Core.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Services.Commands.AccountManagerCommand
{
    public class CloseAccountCommand : ICommand
    {
        public string GetActionName()
        {
            return "Закрытие счета";
        }

        private List<Account> _accounts;
        private string _accountNumber;
        private Account _removedAccount;

        public CloseAccountCommand(List<Account> accounts, string accountNumber)
        {
            _accounts = accounts;
            _accountNumber = accountNumber;
        }

        public void Execute()
        {
            var account = Account.FindAccount(_accounts, _accountNumber);
            if (account != null)
            {
                _removedAccount = account;
                _accounts.Remove(account);
                Console.WriteLine($"Счет {_accountNumber} закрыт.");
            }
            else
            {
                Console.WriteLine("Счет не найден.");
            }
        }

        public void Undo()
        {
            if (_removedAccount != null)
            {
                _accounts.Add(_removedAccount);
                Console.WriteLine($"Счет {_removedAccount.AccountNumber} восстановлен.");
            }
        }
    }
}
