using BankingSystem.BankingSystem.Core.Actions;
using BankingSystem.BankingSystem.Core.Commands;
using BankingSystem.BankingSystem.Core.Commands.SpecialistCommands;
using BankingSystem.BankingSystem.Core.Entities.Banks;
using BankingSystem.BankingSystem.Core.Entities.Enterprises;
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
    public class Specialist : User
    {
        private EnterpriseService enterpriseService;
        private bool isEnterpriseRegistered = false;
        internal ConcreteEnterprise enterprise;
        private SpecialistActions specialistActions;
        private CommandInvoker transactionInvoker;

        public Specialist(string fullName, string passportNumber, string idNumber, string phone, string email, string password, CommandInvoker transactionInvoker)
            : base(fullName, passportNumber, idNumber, phone, email, password, UserRole.Specialist)
        {
            enterpriseService = new EnterpriseService();
            specialistActions = new SpecialistActions(enterpriseService, this, transactionInvoker);
            this.transactionInvoker = transactionInvoker;
        }

        public override void PerformRoleActions()
        {
            Console.WriteLine("1 - Зарегистрировать предприятие");
            Console.WriteLine("2 - Подать заявку на зарплатный проект");
            Console.WriteLine("3 - Оформить зарплатный проект (после одобрения)");
            Console.WriteLine("4 - Перевести средства сотруднику");
            Console.WriteLine("5 - Пополнить счет предприятия");
            Console.WriteLine("6 - Выйти");
        }

        public override void HandleAction(string choice)
        {
            switch (choice)
            {
                case "1":
                    var registerEnterpriseCommand = new RegisterEnterpriseCommand(specialistActions, Program.selectedBank);
                    transactionInvoker.ExecuteCommand(registerEnterpriseCommand);
                    enterprise = registerEnterpriseCommand.Enterprise;
                    isEnterpriseRegistered = true;
                    break;
                case "2":
                    EnsureEnterpriseRegistered();
                    var applyForSalaryProjectCommand = new ApplyForSalaryProjectCommand(specialistActions, enterprise);
                    transactionInvoker.ExecuteCommand(applyForSalaryProjectCommand);
                    break;
                case "3":
                    EnsureEnterpriseRegistered();
                    var finalizeSalaryProjectCommand = new FinalizeSalaryProjectCommand(specialistActions, enterprise);
                    transactionInvoker.ExecuteCommand(finalizeSalaryProjectCommand);
                    break;
                case "4":
                    EnsureEnterpriseRegistered();
                    Console.Write("Введите ФИО сотрудника: ");
                    string employeeName = Console.ReadLine();
                    Console.Write("Введите сумму перевода: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                    {
                        var transferFundsToEmployeeCommand = new TransferFundsToEmployeeCommand(specialistActions, employeeName, amount);
                        transactionInvoker.ExecuteCommand(transferFundsToEmployeeCommand);
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод суммы.");
                    }
                    break;
                case "5":
                    EnsureEnterpriseRegistered();
                    Console.Write("Введите сумму для пополнения счета: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
                    {
                        var depositToEnterpriseAccountCommand = new DepositToEnterpriseAccountCommand(specialistActions, enterprise, depositAmount);
                        transactionInvoker.ExecuteCommand(depositToEnterpriseAccountCommand);
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод суммы.");
                    }
                    break;
                case "6":
                    // Выход
                    break;
                default:
                    Console.WriteLine("Некорректный ввод.");
                    break;
            }
        }

        private void EnsureEnterpriseRegistered()
        {
            if (!isEnterpriseRegistered)
            {
                Console.WriteLine("Пожалуйста, зарегистрируйте предприятие.");
                var registerEnterpriseCommand = new RegisterEnterpriseCommand(specialistActions, Program.selectedBank);
                transactionInvoker.ExecuteCommand(registerEnterpriseCommand);
                enterprise = registerEnterpriseCommand.Enterprise;
                isEnterpriseRegistered = true;
            }
        }
    }
}