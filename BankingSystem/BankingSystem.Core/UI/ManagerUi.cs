using BankingSystem.BankingSystem.Core.Entities.Users;
using BankingSystem.BankingSystem.Core.Services.Commands.ManagerActionsCommand;
using BankingSystem.BankingSystem.Core.Services;
using BankingSystem.BankingSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.UI
{
    public class ManagerUi : IUserUi
    {
        private Manager _manager;
        private LogDatabase _logDb;

        public ManagerUi(Manager manager)
        {
            _manager = manager;
            _logDb = new LogDatabase();
        }

        public void PerformRoleActions()
        {
            Console.WriteLine("1 - Одобрить регистрацию клиентов");
            Console.WriteLine("2 - Подтвердить кредиты");
            Console.WriteLine("3 - Подтвердить рассрочку");
            Console.WriteLine("4 - Отменить операции специалиста");
            Console.WriteLine("5 - Просмотреть статистику по движениям средств");
            Console.WriteLine("6 - Подтвердить заявку на зарплатный проект");
            Console.WriteLine("7 - Отменить перевод средств");
            Console.WriteLine("0 - Выйти в главное меню");
        }

        public void HandleAction(string choice)
        {
            switch (choice)
            {
                case "1":
                    var approveClientRegistrationsCommand = new ApproveClientRegistrationsCommand(_manager.AuthService);
                    _manager.TransactionInvoker.ExecuteCommand(approveClientRegistrationsCommand);
                    _logDb.AddLog(_manager.FullName, "Одобрил регистрацию клиента");
                    break;
                case "2":
                    var approveLoanApplicationsCommand = new ApproveLoanApplicationsCommand(_manager.AuthService);
                    _manager.TransactionInvoker.ExecuteCommand(approveLoanApplicationsCommand);
                    _logDb.AddLog(_manager.FullName, "Подтвердил кредит");
                    break;
                case "3":
                    var approveInstallmentApplicationsCommand = new ApproveInstallmentApplicationsCommand(_manager.AuthService);
                    _manager.TransactionInvoker.ExecuteCommand(approveInstallmentApplicationsCommand);
                    _logDb.AddLog(_manager.FullName, "Подтвердил рассрочку");
                    break;
                case "4":
                    ManagerActions.ShowSpecialistCommandHistoryAndUndo(_manager.TransactionInvoker);
                    _logDb.AddLog(_manager.FullName, "Отменил операцию специалиста");
                    break;
                case "5":
                    TransactionDatabase transactionDb = new TransactionDatabase();
                    transactionDb.ShowTransactions();
                    break;
                case "6":
                    OperatorActions.SelectSalaryProjectApplication(_manager.TransactionInvoker);
                    _logDb.AddLog(_manager.FullName, "Подтвердил зарплатный проект");
                    break;
                case "7":
                    OperatorActions.ShowFilteredCommandHistoryAndUndo(_manager.TransactionInvoker);
                    _logDb.AddLog(_manager.FullName, "Отменил перевод средств");
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                    break;
            }
        }
    }
}
