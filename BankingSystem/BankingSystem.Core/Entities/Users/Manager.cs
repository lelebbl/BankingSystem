using BankingSystem.BankingSystem.Core.Commands;
using BankingSystem.BankingSystem.Core.Entities.Operations;
using BankingSystem.BankingSystem.Core.Enums;
using BankingSystem.BankingSystem.Core.Services;
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
            Console.WriteLine("4 - Отменить операцию специалиста");
            Console.WriteLine("0 - Выйти в главное меню");
        }

        public override void HandleAction(string choice)
        {
            switch (choice)
            {
                case "1":
                    ApproveClientRegistrations();
                    break;
                case "2":
                    ApproveLoanApplications();
                    break;
                case "3":
                    ApproveInstallmentApplications();
                    break;
                case "4":
                    
                    break;
                case "0":
                   
                    break;
                default:
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                    break;
            }
        }

        private void ApproveClientRegistrations()
        {
            Console.WriteLine("Ожидание списка клиентов...");
            var pendingUsers = _authService.GetPendingUsers();
            if (pendingUsers.Count == 0)
            {
                Console.WriteLine("Нет клиентов, ожидающих одобрения.");
            }
            else
            {
                Console.WriteLine("Клиенты на одобрение:");
                for (int i = 0; i < pendingUsers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {pendingUsers[i].FullName} ({pendingUsers[i].Email})");
                }

                Console.Write("Введите номер клиента для одобрения: ");
                if (int.TryParse(Console.ReadLine(), out int userIndex) && userIndex > 0 && userIndex <= pendingUsers.Count)
                {
                    _authService.ApproveUser(pendingUsers[userIndex - 1]);
                    Console.WriteLine("Клиент одобрен!");
                }
            }
        }

        private void ApproveLoanApplications()
        {
            Console.WriteLine("Подтверждение заявок на кредиты...");
            var pendingLoans = GetPendingApplications<Loan>();

            if (pendingLoans.Count == 0)
            {
                Console.WriteLine("Нет заявок на кредиты, ожидающих одобрения.");
            }
            else
            {
                foreach (var loan in pendingLoans)
                {
                    Console.WriteLine($"Заявка от {loan.Applicant.FullName} на сумму {loan.Amount} руб. сроком на {loan.TermMonths} месяцев.");
                    Console.Write("Одобрить заявку? (да/нет): ");
                    if (Console.ReadLine().ToLower() == "да")
                    {
                        loan.Approve();
                    }
                }
            }
        }

        private void ApproveInstallmentApplications()
        {
            Console.WriteLine("Подтверждение заявок на рассрочку...");
            var pendingInstallments = GetPendingApplications<Installment>();

            if (pendingInstallments.Count == 0)
            {
                Console.WriteLine("Нет заявок на рассрочку, ожидающих одобрения.");
            }
            else
            {
                foreach (var installment in pendingInstallments)
                {
                    Console.WriteLine($"Заявка от {installment.Applicant.FullName} на сумму {installment.Amount} руб. сроком на {installment.TermMonths} месяцев.");
                    Console.Write("Одобрить заявку? (да/нет): ");
                    if (Console.ReadLine().ToLower() == "да")
                    {
                        installment.Approve();
                    }
                }
            }
        }

        private List<T> GetPendingApplications<T>() where T : Application
        {
            var allClients = _authService.GetAllClients();
            var pendingApplications = new List<T>();

            foreach (var client in allClients)
            {
                foreach (var application in client.applications)
                {
                    if (application is T && !application.IsApproved)
                    {
                        pendingApplications.Add((T)application);
                    }
                }
            }

            return pendingApplications;
        }
    }


}

