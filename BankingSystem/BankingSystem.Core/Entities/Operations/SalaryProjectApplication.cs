using BankingSystem.BankingSystem.Core.Services.Commands;
using BankingSystem.BankingSystem.Core.Entities.Users;
using BankingSystem.BankingSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Entities.Operations
{
    public class SalaryProjectApplication
    {
        public ConcreteEnterprise Enterprise { get; }
        public List<string> EmployeeNames { get; private set; }
        public bool IsApproved { get; private set; }

        public SalaryProjectApplication(ConcreteEnterprise enterprise)
        {
            Enterprise = enterprise;
            EmployeeNames = new List<string>();
            IsApproved = false;
        }

        public void Approve()
        {
            IsApproved = true;
            Console.WriteLine($"Заявка на зарплатный проект для предприятия {Enterprise.LegalName} одобрена.");
        }

        public void Reject()
        {
            IsApproved = false;
            Console.WriteLine($"Заявка на зарплатный проект для предприятия {Enterprise.LegalName} отклонена.");
        }


        public void SetEmployeeNames(List<string> employeeNames)
        {
            EmployeeNames = employeeNames;
        }

        public bool Execute()
        {
            if (!IsApproved)
            {
                Console.WriteLine("Заявка на зарплатный проект не одобрена.");
                return false;
            }

            var transactionInvoker = new CommandInvoker();
            var enterpriseService = new EnterpriseService();
            var specialist = new Specialist("Specialist Name", "Passport Number", "ID Number", "Phone", "Email", "Password", transactionInvoker);

            var specialistActions = new SpecialistActions(enterpriseService, specialist, transactionInvoker);

            foreach (var employeeName in EmployeeNames)
            {
                var client = Program.selectedBank.Clients.FirstOrDefault(c => c.FullName == employeeName);
                if (client == null)
                {
                    Console.WriteLine($"Сотрудник {employeeName} не найден. Пожалуйста, зарегистрируйте сотрудника.");
                    client = specialistActions.RegisterNewClient(employeeName);
                }

                var existingAccount = client.accounts.FirstOrDefault();
                string accountNumber = existingAccount?.AccountNumber ?? specialistActions.OpenEmployeeAccount(employeeName, client);

                Console.Write("Введите сумму перевода: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    if (Enterprise.Balance >= amount)
                    {
                        Enterprise.Withdraw(amount);
                        client.Deposit(accountNumber, amount);
                        Console.WriteLine($"Перевод {amount} руб. выполнен успешно на счет сотрудника {employeeName}.");
                    }
                    else
                    {
                        Console.WriteLine("Недостаточно средств.");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод суммы.");
                    return false;
                }
            }
            return true;
        }

        public void Cancel()
        {
            if (IsApproved)
            {
                IsApproved = false;
                Console.WriteLine($"Заявка на зарплатный проект для предприятия {Enterprise.LegalName} отменена.");
            }
            else
            {
                Console.WriteLine("Заявка на зарплатный проект не была одобрена.");
            }
        }
    }
}
