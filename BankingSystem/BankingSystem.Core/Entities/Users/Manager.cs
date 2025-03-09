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
            Console.WriteLine("4 - Отменить последнее действие пользователя");
            Console.WriteLine("0 - Выйти в главное меню");
        }

        public override void HandleAction(string choice)
        {
            switch (choice)
            {
                case "1":
                    ManagerActions.ApproveClientRegistrations(_authService);
                    break;
                case "2":
                    ManagerActions.ApproveLoanApplications(_authService);
                    break;
                case "3":
                    ManagerActions.ApproveInstallmentApplications(_authService);
                    break;
                case "4":
                    ManagerActions.UndoLastUserAction(_transactionInvoker);
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

