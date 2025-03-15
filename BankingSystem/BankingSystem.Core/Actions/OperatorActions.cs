using BankingSystem.BankingSystem.Core.Commands;
using BankingSystem.BankingSystem.Core.Commands.OperationsCommand;
using BankingSystem.BankingSystem.Core.Commands.OperatorActionsCommand;
using BankingSystem.BankingSystem.Core.Commands.SpecialistCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Actions
{
    public static class OperatorActions
    {
        public static void SelectSalaryProjectApplication(CommandInvoker transactionInvoker)
        {
            var applications = Program.selectedBank.GetAllSalaryProjectApplications();
            if (applications.Count == 0)
            {
                Console.WriteLine("Нет заявок на зарплатный проект.");
                return;
            }

            Console.WriteLine("Список заявок на зарплатный проект:");
            for (int i = 0; i < applications.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Предприятие: {applications[i].Enterprise.LegalName}");
            }

            Console.Write("Введите номер заявки для одобрения: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= applications.Count)
            {
                var command = new ApproveSalaryProjectApplicationCommand(applications, index - 1);
                transactionInvoker.ExecuteCommand(command);
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
        }

        public static void ShowFilteredCommandHistoryAndUndo(CommandInvoker transactionInvoker)
        {
            var history = transactionInvoker.GetCommandHistory();

            var filteredCommands = history
                .Where(cmd => cmd is TransactionCommand)
                .ToList();

            if (filteredCommands.Count == 0)
            {
                Console.WriteLine("Нет подходящих команд для отмены.");
                return;
            }

            Console.WriteLine("Доступные команды для отмены:");
            for (int i = 0; i < filteredCommands.Count; i++)
            {
                Console.WriteLine($"{i} - {filteredCommands[i].GetActionName()}");
            }

            Console.Write("Введите индекс команды для отмены: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < filteredCommands.Count)
            {
                transactionInvoker.UndoCommand(filteredCommands[index]);
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
        }
    }
}
