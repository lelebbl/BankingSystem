using BankingSystem.BankingSystem.Core.Entities.Accounts;
using BankingSystem.BankingSystem.Core.Entities.Banks;
using BankingSystem.BankingSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Entities.Enterprises
{
    public class ConcreteEnterprise : Enterprise
    {
        private EnterpriseService enterpriseService;
        private Bank bank;

        public ConcreteEnterprise(string type, string legalName, string unp, string legalAddress, Bank bank, string bik, EnterpriseService enterpriseService)
        : base(type, legalName, unp, legalAddress, bank, bik)
        {
            this.bank = bank;
            this.enterpriseService = enterpriseService;
            this.Balance = 0;
        }

        public static ConcreteEnterprise RegisterEnterprise(EnterpriseService enterpriseService, Bank bank)
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
            return newEnterprise;
        }

        public void SubmitSalaryProjectApplication(List<string> employeeNames)
        {
            Employees = employeeNames.Select(en => en.Split(':')[0]).ToList();
            Console.WriteLine($"Предприятие {LegalName} подало заявку на зарплатный проект в банке {Bank.Name}.");

            HashSet<string> processedEmployees = new HashSet<string>();

            foreach (var employeeData in employeeNames)
            {
                var employeeInfo = employeeData.Split(':');
                string employeeName = employeeInfo[0];
                string accountNumber = employeeInfo.Length > 2 ? employeeInfo[1] : null;
                decimal amount = employeeInfo.Length > 2 ? decimal.Parse(employeeInfo[2]) : decimal.Parse(employeeInfo[1]);

                if (!processedEmployees.Contains(employeeName))
                {
                    processedEmployees.Add(employeeName);

                    if (string.IsNullOrEmpty(accountNumber))
                    {
                        Console.WriteLine($"У сотрудника {employeeName} нет счета в банке. Предлагаем открыть счет.");
                        accountNumber = OpenEmployeeAccount(employeeName);
                    }

                    var employeeAccount = Accounts.FirstOrDefault(account => account.AccountNumber == accountNumber);
                    if (employeeAccount == null)
                    {
                        Console.WriteLine($"Номер счета {accountNumber} не найден. Предлагаем открыть новый счет для сотрудника {employeeName}.");
                        accountNumber = OpenEmployeeAccount(employeeName);
                    }

                    var client = Bank.Clients.FirstOrDefault(c => c.FullName == employeeName);
                    if (client != null)
                    {
                        client.Deposit(accountNumber, amount);
                    }
                    else
                    {
                        Console.WriteLine($"Ошибка: клиент {employeeName} не найден.");
                    }
                }
            }
        }

        private string OpenEmployeeAccount(string employeeName)
        {
            Console.WriteLine($"Открытие счета для сотрудника {employeeName}...");
            string accountNumber = Guid.NewGuid().ToString();
            Accounts.Add(new CheckingAccount(accountNumber, 0));
            Console.WriteLine($"Счет открыт для сотрудника {employeeName}. Номер счета: {accountNumber}");
            return accountNumber;
        }
    }
}
