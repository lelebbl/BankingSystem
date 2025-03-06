using BankingSystem.BankingSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Commands
{
    public class TransactionInvoker
    {
        private readonly Stack<ITransactionCommand> _transactionHistory;
        private bool _hasCancelledTransaction;

        public TransactionInvoker()
        {
            _transactionHistory = new Stack<ITransactionCommand>();
            _hasCancelledTransaction = false;
        }

        public void ExecuteCommand(ITransactionCommand command)
        {
            command.Execute();
            _transactionHistory.Push(command);
        }

        public void UndoLastCommand()
        {
            if (!_hasCancelledTransaction && _transactionHistory.Count > 0)
            {
                ITransactionCommand command = _transactionHistory.Pop();
                command.Undo();
                _hasCancelledTransaction = true;
            }
            else if (_hasCancelledTransaction)
            {
                Console.WriteLine("Вы уже отменили одну транзакцию. Больше нельзя отменять транзакции.");
            }
            else
            {
                Console.WriteLine("Нет транзакций для отмены.");
            }
        }

        public void UndoAllCommands()
        {
            while (_transactionHistory.Count > 0)
            {
                ITransactionCommand command = _transactionHistory.Pop();
                command.Undo();
            }
            Console.WriteLine("Все действия отменены.");
        }
    }
}
