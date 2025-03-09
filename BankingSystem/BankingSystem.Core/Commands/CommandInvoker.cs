using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Commands
{
    public class CommandInvoker
    {
        private readonly Stack<ICommand> _transactionHistory;

        public CommandInvoker()
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

        public List<ICommand> GetCommandHistory()
        {
            return new List<ICommand>(_transactionHistory);
        }

        public void UndoCommandAtIndex(int index)
        {
            if (index >= 0 && index < _transactionHistory.Count)
            {
                var commands = _transactionHistory.ToArray();
                var command = commands[index];
                command.Undo();

                var newStack = new Stack<ICommand>();
                for (int i = 0; i < _transactionHistory.Count; i++)
                {
                    if (i != index)
                    {
                        newStack.Push(commands[i]);
                    }
                }
                _transactionHistory.Clear();
                foreach (var cmd in newStack)
                {
                    _transactionHistory.Push(cmd);
                }
                Console.WriteLine($"Команда под индексом {index} отменена.");
            }
            else
            {
                Console.WriteLine("Неверный индекс команды.");
            }
        }

        public void UndoCommand(ICommand command)
        {
            if (_transactionHistory.Contains(command))
            {
                var newStack = new Stack<ICommand>(_transactionHistory.Where(c => c != command));
                _transactionHistory.Clear();
                foreach (var cmd in newStack)
                {
                    _transactionHistory.Push(cmd);
                }

                command.Undo();
                Console.WriteLine("Команда отменена.");
            }
            else
            {
                Console.WriteLine("Команда не найдена в истории.");
            }
        }

    }
}

