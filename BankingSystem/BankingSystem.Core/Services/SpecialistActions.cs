using BankingSystem.BankingSystem.Core.Entities.Accounts;
using BankingSystem.BankingSystem.Core.Entities.Banks;
using BankingSystem.BankingSystem.Core.Entities.Enterprises;
using BankingSystem.BankingSystem.Core.Entities.Users;
using BankingSystem.BankingSystem.Core.Entities.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.BankingSystem.Core.Commands;

namespace BankingSystem.BankingSystem.Core.Services
{
    public class SpecialistActions
    {
        private EnterpriseService enterpriseService;
        private Specialist specialist;
        private TransactionInvoker transactionInvoker;

        public SpecialistActions(EnterpriseService enterpriseService, Specialist specialist, TransactionInvoker transactionInvoker)
        {
            this.enterpriseService = enterpriseService;
            this.specialist = specialist;
            this.transactionInvoker = transactionInvoker;
        }

        public bool CheckIfEnterpriseExists()
        {
            Console.Write("Введите юридическое название предприятия: ");
            string legalName = Console.ReadLine();
            var existingEnterprise = enterpriseService.FindEnterpriseByName(legalName);
            if (existingEnterprise != null)
            {
                Console.WriteLine($"Предприятие {legalName} уже зарегистрировано.");
                return true;
            }
            return false;
        }

        public ConcreteEnterprise RegisterEnterprise(Bank bank)
        {
            Console.Write("Введите тип предприятия (ИП, ООО, ЗАО): ");
            string type = Console.ReadLine();
            Console.Write("Введите юридическое название предприятия: ");
            string legalName = Console.ReadLine();
            Console.Write("Введите УНП: ");
            string unp = Console.ReadLine();
            Console.Write("Введите юридический адрес: ");
            string legalAddress = Console.ReadLine();
            Console.Write("Введите БИК банка: ");
            string bin = Console.ReadLine();

            ConcreteEnterprise newEnterprise = new ConcreteEnterprise(type, legalName, unp, legalAddress, bank, bin, enterpriseService);
            enterpriseService.RegisterEnterprise(newEnterprise);

            Console.WriteLine($"Предприятие {legalName} зарегистрировано.");
            return newEnterprise;
        }

        public void ApplyForSalaryProject(ConcreteEnterprise enterprise)
        {
            SalaryProjectApplication application = new SalaryProjectApplication(enterprise);
            Program.selectedBank.AddSalaryProjectApplication(application);
            Console.WriteLine("Заявка на зарплатный проект подана и ожидает одобрения оператором.");
        }

        public void FinalizeSalaryProject(ConcreteEnterprise enterprise)
        {
            var application = Program.selectedBank.GetSalaryProjectApplication(enterprise.LegalName);
            if (application == null || !application.IsApproved)
            {
                Console.WriteLine("Заявка на зарплатный проект не найдена или не одобрена.");
                return;
            }

            Console.Write("Введите количество сотрудников: ");
            if (int.TryParse(Console.ReadLine(), out int employeeCount))
            {
                List<string> employeeNames = new List<string>();
                for (int i = 0; i < employeeCount; i++)
                {
                    Console.Write($"Введите ФИО сотрудника {i + 1}: ");
                    string employeeName = Console.ReadLine();
                    employeeNames.Add(employeeName);
                }

                application.SetEmployeeNames(employeeNames);
                if (application.Execute())
                {
                    Console.WriteLine("Зарплатный проект оформлен.");
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
            }
        }

        public Client RegisterNewClient(string employeeName)
        {
            Console.Write("Введите паспортные данные сотрудника: ");
            string passportNumber = Console.ReadLine();
            Console.Write("Введите идентификационный номер сотрудника: ");
            string idNumber = Console.ReadLine();
            Console.Write("Введите телефон сотрудника: ");
            string phone = Console.ReadLine();
            Console.Write("Введите email сотрудника: ");
            string email = Console.ReadLine();
            Console.Write("Введите пароль для сотрудника: ");
            string password = Console.ReadLine();

            Client newClient = new Client(employeeName, passportNumber, idNumber, phone, email, password, transactionInvoker);
            Program.selectedBank.RegisterClient(newClient);
            return newClient;
        }

        public string OpenEmployeeAccount(string employeeName, Client client)
        {
            Console.WriteLine($"Открытие счета для сотрудника {employeeName}...");
            string accountNumber = Guid.NewGuid().ToString();
            var newAccount = new CheckingAccount(accountNumber, 0);
            client.accounts.Add(newAccount);
            Console.WriteLine($"Счет открыт. Номер счета: {accountNumber}");
            return accountNumber;
        }

        public void DepositToEnterpriseAccount(ConcreteEnterprise enterprise)
        {
            Console.Write("Введите сумму для пополнения счета: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                enterprise.Deposit(amount);
                Console.WriteLine($"Счет предприятия пополнен на {amount} руб.");
            }
            else
            {
                Console.WriteLine("Некорректная сумма.");
            }
        }

        public void TransferFundsToEmployee()
        {
            Console.Write("Введите ФИО сотрудника: ");
            string employeeName = Console.ReadLine();
            var client = Program.selectedBank.Clients.FirstOrDefault(c => c.FullName == employeeName);

            if (client == null)
            {
                Console.WriteLine("Сотрудник не найден. Пожалуйста, зарегистрируйте сотрудника.");
                client = RegisterNewClient(employeeName);
            }

            var existingAccount = client.accounts.FirstOrDefault();
            string accountNumber = existingAccount?.AccountNumber ?? OpenEmployeeAccount(employeeName, client);

            Console.Write("Введите сумму перевода: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && specialist.enterprise.Balance >= amount)
            {
                specialist.enterprise.Withdraw(amount);
                client.Deposit(accountNumber, amount);
                Console.WriteLine($"Перевод {amount} руб. выполнен успешно на счет сотрудника {employeeName}.");
            }
            else
            {
                Console.WriteLine("Недостаточно средств или некорректный ввод суммы.");
            }
        }
    }
}
