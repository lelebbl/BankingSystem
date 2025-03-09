using BankingSystem.BankingSystem.Core.Commands;
using BankingSystem.BankingSystem.Core.Entities.Operations;
using BankingSystem.BankingSystem.Core.Enums;
using BankingSystem.BankingSystem.Core.Services;
using BankingSystem.BankingSystem.Core.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.BankingSystem.Data;
using BankingSystem.BankingSystem.Core.Commands.ManagerActionsCommand;

namespace BankingSystem.BankingSystem.Core.Entities.Users
{
    public class Manager : User
    {
        private readonly AuthService _authService;
        private CommandInvoker _transactionInvoker;

        public Manager(string fullName, string passportNumber, string idNumber, string phone, string email, string password, AuthService authService)
            : base(fullName, passportNumber, idNumber, phone, email, password, UserRole.Manager)
        {
            _authService = authService;
            _transactionInvoker = new CommandInvoker();
        }

        public override void PerformRoleActions()
        {
            Console.WriteLine("1 - Одобрить регистрацию клиентов");
            Console.WriteLine("2 - Подтвердить кредиты");
            Console.WriteLine("3 - Подтвердить рассрочку");
            Console.WriteLine("4 - Отменить операции специалиста");
            Console.WriteLine("0 - Выйти в главное меню");
        }

        public override void HandleAction(string choice)
        {
            LogDatabase logDb = new LogDatabase();

            switch (choice)
            {
                case "1":
                    var approveClientRegistrationsCommand = new ApproveClientRegistrationsCommand(_authService);
                    _transactionInvoker.ExecuteCommand(approveClientRegistrationsCommand);
                    logDb.AddLog(FullName, "Одобрил регистрацию клиента");
                    break;
                case "2":
                    var approveLoanApplicationsCommand = new ApproveLoanApplicationsCommand(_authService);
                    _transactionInvoker.ExecuteCommand(approveLoanApplicationsCommand);
                    logDb.AddLog(FullName, "Подтвердил кредит");
                    break;
                case "3":
                    var approveInstallmentApplicationsCommand = new ApproveInstallmentApplicationsCommand(_authService);
                    _transactionInvoker.ExecuteCommand(approveInstallmentApplicationsCommand);
                    logDb.AddLog(FullName, "Подтвердил рассрочку");
                    break;
                case "4":
                    ManagerActions.ShowSpecialistCommandHistoryAndUndo(_transactionInvoker);
                    logDb.AddLog(FullName, "Отменил операцию специалиста");
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

