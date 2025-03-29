using BankingSystem.BankingSystem.Core.Entities.Users;
using BankingSystem.BankingSystem.Core.Services;
using BankingSystem.BankingSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.UI
{
    public class AdministratorUi : IUserUi
    {
        private Administrator _administrator;
        private LogDatabase _logDb;

        public AdministratorUi(Administrator administrator)
        {
            _administrator = administrator;
            _logDb = new LogDatabase();
        }

        public void PerformRoleActions()
        {
            Console.WriteLine("1 - Просмотр всех логов действий");
            Console.WriteLine("2 - Отмена всех действий пользователей");
            Console.WriteLine("3 - Отмена последнего действия");
            Console.WriteLine("4 - Отмена выбранного действия");
            Console.WriteLine("0 - Выйти");
        }

        public void HandleAction(string choice)
        {
            switch (choice)
            {
                case "1":
                    Console.WriteLine("\nИстория логов:");
                    _logDb.ShowLogs();
                    break;
                case "2":
                    _administrator.TransactionInvoker.UndoAllCommands();
                    Console.WriteLine("Все действия пользователей отменены.");
                    break;
                case "3":
                    _administrator.TransactionInvoker.UndoLastCommand();
                    Console.WriteLine("Последнее действие отменено.");
                    break;
                case "4":
                    AdministratorActions.ShowCommandHistoryAndUndo(_administrator.TransactionInvoker);
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
