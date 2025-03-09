using BankingSystem.BankingSystem.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Actions
{
    public static class AdministratorActions
    {
        public static void ShowCommandHistoryAndUndo(CommandInvoker transactionInvoker)
        {
            var history = transactionInvoker.GetCommandHistory();
            if (history.Count == 0)
            {
                Console.WriteLine("Нет выполненных команд.");
                return;
            }

            Console.WriteLine("Выполненные команды:");
            for (int i = 0; i < history.Count; i++)
            {
                Console.WriteLine($"{i} - {history[i].GetActionName()}");
            }

            Console.Write("Введите индекс команды для отмены: ");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                transactionInvoker.UndoCommandAtIndex(index);
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
        }
    }
}
