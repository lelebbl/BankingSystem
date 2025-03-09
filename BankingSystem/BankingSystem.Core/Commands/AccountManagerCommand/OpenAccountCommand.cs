using BankingSystem.BankingSystem.Core.Entities.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Commands.AccountManagerCommand
{
    public class OpenAccountCommand : ICommand
    {
        public string GetActionName()
        {
            return "Открытие счета";
        }

        private List<Account> _accounts;
        private Account _createdAccount;

        public OpenAccountCommand(List<Account> accounts)
        {
            _accounts = accounts;
        }

        public void Execute()
        {
            string accountNumber = Guid.NewGuid().ToString();
            _createdAccount = new CheckingAccount(accountNumber, 0);
            _accounts.Add(_createdAccount);
            Console.WriteLine($"Счет открыт. Номер счета: {accountNumber}");
        }

        public void Undo()
        {
            if (_createdAccount != null)
            {
                _accounts.Remove(_createdAccount);
                Console.WriteLine($"Счет {_createdAccount.AccountNumber} закрыт.");
            }
        }
    }
}
