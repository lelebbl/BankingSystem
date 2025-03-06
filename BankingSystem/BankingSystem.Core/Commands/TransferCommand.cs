using BankingSystem.BankingSystem.Core.Entities.Accounts;
using BankingSystem.BankingSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Commands
{
    public class TransferCommand : ICommand
    {
        private readonly Account _fromAccount;
        private readonly Account _toAccount;
        private readonly decimal _amount;
        private bool _isExecuted;

        public TransferCommand(Account fromAccount, Account toAccount, decimal amount)
        {
            _fromAccount = fromAccount;
            _toAccount = toAccount;
            _amount = amount;
            _isExecuted = false;
        }

        public void Execute()
        {
            if (_fromAccount.Balance >= _amount)
            {
                _fromAccount.Withdraw(_amount);
                _toAccount.Deposit(_amount);
                _isExecuted = true;
                LogManager.Instance.Log($"Transfer of {_amount} from {_fromAccount.AccountNumber} to {_toAccount.AccountNumber} executed.");
            }
            else
            {
                Console.WriteLine("Недостаточно средств.");
            }
        }

        public void Undo()
        {
            if (_isExecuted)
            {
                _toAccount.Withdraw(_amount);
                _fromAccount.Deposit(_amount);
                _isExecuted = false;
                LogManager.Instance.Log($"Transfer of {_amount} from {_toAccount.AccountNumber} to {_fromAccount.AccountNumber} undone.");
            }
            else
            {
                Console.WriteLine("Транзакция не была выполнена, отмена невозможна.");
            }
        }
    }
}
