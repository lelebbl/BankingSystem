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
        private TransactionInvoker _transactionInvoker;
        private LogDatabase logDb;

        public Administrator(string fullName, string passportNumber, string idNumber, string phone, string email, string password)
            : base(fullName, passportNumber, idNumber, phone, email, password, UserRole.Administrator)
        {
            _transactionInvoker = new TransactionInvoker();
            logDb = new LogDatabase();
        }

        public override void PerformRoleActions()
        {
            Console.WriteLine("1 - Просмотр всех логов действий");
            Console.WriteLine("2 - Отмена всех действий пользователей");
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
                    Console.WriteLine("Все действия пользователей отменены.");
                    break;
                case "0":
                    // Exit
                    break;
                default:
                    Console.WriteLine("Некорректный ввод.");
                    break;
            }
        } 
    }
}
