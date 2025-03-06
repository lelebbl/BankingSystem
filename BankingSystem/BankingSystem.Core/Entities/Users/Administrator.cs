using BankingSystem.BankingSystem.Core.Commands;
using BankingSystem.BankingSystem.Core.Enums;
using BankingSystem.BankingSystem.Core.Services;
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

        public Administrator(string fullName, string passportNumber, string idNumber, string phone, string email, string password)
            : base(fullName, passportNumber, idNumber, phone, email, password, UserRole.Administrator)
        {
            _transactionInvoker = new TransactionInvoker();
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
                    ViewAllLogs();
                    break;
                case "2":
                    CancelAllActions();
                    break;
                case "0":
                    // Exit
                    break;
                default:
                    Console.WriteLine("Некорректный ввод.");
                    break;
            }
        }

        private void ViewAllLogs()
        {
            LogManager.Instance.ViewLogs();
        }

        private void CancelAllActions()
        {
            _transactionInvoker.UndoAllCommands();
        }
    }
}
