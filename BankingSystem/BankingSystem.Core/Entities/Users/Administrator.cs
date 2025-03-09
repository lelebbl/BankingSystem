using BankingSystem.BankingSystem.Core.Actions;
using BankingSystem.BankingSystem.Core.Commands;
using BankingSystem.BankingSystem.Core.Enums;
using BankingSystem.BankingSystem.Core.Services;
using BankingSystem.BankingSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Entities.Users
{
    public class Administrator : User
    {
        private CommandInvoker _transactionInvoker;
        private LogDatabase logDb;

        public Administrator(string fullName, string passportNumber, string idNumber, string phone, string email, string password, CommandInvoker transactionInvoker)
            : base(fullName, passportNumber, idNumber, phone, email, password, UserRole.Administrator)
        {
            _transactionInvoker = transactionInvoker;
            logDb = new LogDatabase();
        }

        public override void PerformRoleActions()
        {
            Console.WriteLine("1 - Просмотр всех логов действий");
            Console.WriteLine("2 - Отмена всех действий пользователей");
            Console.WriteLine("3 - Отмена последнего действия");
            Console.WriteLine("4 - Отмена выбранного действия");
            Console.WriteLine("0 - Выйти");
        }

        public override void HandleAction(string choice)
        {
            switch (choice)
            {
                case "1":
                    Console.WriteLine("\nИстория логов:");
                    logDb.ShowLogs();
                    break;
                case "2":
                    _transactionInvoker.UndoAllCommands();
                    break;
                case "3":
                    _transactionInvoker.UndoLastCommand();
                    Console.WriteLine("Последнее действие отменено.");
                    break;
                case "4":
                    AdministratorActions.ShowCommandHistoryAndUndo(_transactionInvoker);
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Некорректный ввод.");
                    break;
            }
        }
    }
}

