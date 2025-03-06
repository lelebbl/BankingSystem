using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Commands
{
    public class TransactionInvoker
    {
        private readonly Stack<ICommand> _transactionHistory;

        public TransactionInvoker()
        {
            _transactionHistory = new Stack<ICommand>();
        }

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _transactionHistory.Push(command);
        }

        public void UndoLastCommand()
        {
            if (_transactionHistory.Count > 0)
            {
                ICommand command = _transactionHistory.Pop();
                command.Undo();
            }
            else
            {
                Console.WriteLine("Нет команд для отмены.");
            }
        }

        public void UndoAllCommands()
        {
            while (_transactionHistory.Count > 0)
            {
                ICommand command = _transactionHistory.Pop();
                command.Undo();
            }
            Console.WriteLine("Все действия отменены.");
        }
    }
}
