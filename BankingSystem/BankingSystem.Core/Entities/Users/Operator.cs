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
    public class Operator : User
    {
        private TransactionInvoker _transactionInvoker;

        public Operator(string fullName, string passportNumber, string idNumber, string phone, string email, string password)
            : base(fullName, passportNumber, idNumber, phone, email, password, UserRole.Operator)
        {
            _transactionInvoker = new TransactionInvoker();
        }

        public override void PerformRoleActions()
        {
            Console.WriteLine("1 - Просмотреть статистику");
            Console.WriteLine("2 - Подтвердить заявку на зарплатный проект");
            Console.WriteLine("3 - Отменить перевод (можно только 1 раз)");
            Console.WriteLine("0 - Выйти");
        }

        public override void HandleAction(string choice)
        {
            switch (choice)
            {
                case "1":
                    ViewTransactionLogs();
                    break;
                case "2":
                    SelectSalaryProjectApplication();
                    break;
                case "3":
                    _transactionInvoker.UndoLastCommand();
                    break;
                case "0":
                    // Exit
                    break;
                default:
                    Console.WriteLine("Некорректный ввод.");
                    break;
            }
        }

        private void ViewTransactionLogs()
        {
            LogManager.Instance.ViewLogs();
        }

        private void SelectSalaryProjectApplication()
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
                var application = applications[index - 1];
                application.Approve();
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
            }
        }
    }
}
