using BankingSystem.BankingSystem.Core.Commands;
using BankingSystem.BankingSystem.Core.Entities.Operations;
using BankingSystem.BankingSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Actions
{
    public static class ManagerActions
    {
        public static void ApproveClientRegistrations(AuthService authService)
        {
            Console.WriteLine("Ожидание списка клиентов...");
            var pendingUsers = authService.GetPendingUsers();
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
                    authService.ApproveUser(pendingUsers[userIndex - 1]);
                    Console.WriteLine("Клиент одобрен!");
                }
            }
        }

        public static void ApproveLoanApplications(AuthService authService)
        {
            Console.WriteLine("Подтверждение заявок на кредиты...");
            var pendingLoans = GetPendingApplications<Loan>(authService);

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

        public static void ApproveInstallmentApplications(AuthService authService)
        {
            Console.WriteLine("Подтверждение заявок на рассрочку...");
            var pendingInstallments = GetPendingApplications<Installment>(authService);

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

        private static List<T> GetPendingApplications<T>(AuthService authService) where T : Application
        {
            var allClients = authService.GetAllClients();
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

        public static void UndoLastUserAction(CommandInvoker transactionInvoker)
        {
            transactionInvoker.UndoLastCommand();
            Console.WriteLine("Последнее действие пользователя отменено.");
        }
    }
}
