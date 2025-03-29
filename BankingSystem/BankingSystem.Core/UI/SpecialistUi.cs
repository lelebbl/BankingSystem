using BankingSystem.BankingSystem.Core.Entities.Operations;
using BankingSystem.BankingSystem.Core.Services.Commands.SpecialistCommands;
using BankingSystem.BankingSystem.Core.Services;
using BankingSystem.BankingSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.BankingSystem.Core.Entities.Users;

namespace BankingSystem.BankingSystem.Core.UI
{
    public class SpecialistUi : IUserUi
    {
        private Specialist _specialist;
        private LogDatabase _logDb;

        public SpecialistUi(Specialist specialist)
        {
            _specialist = specialist;
            _logDb = new LogDatabase();
        }

        public void PerformRoleActions()
        {
            Console.WriteLine("1 - Зарегистрировать предприятие");
            Console.WriteLine("2 - Подать заявку на зарплатный проект");
            Console.WriteLine("3 - Оформить зарплатный проект (после одобрения)");
            Console.WriteLine("4 - Перевести средства сотруднику");
            Console.WriteLine("5 - Пополнить счет предприятия");
            Console.WriteLine("0 - Выйти");
        }

        public void HandleAction(string choice)
        {
            switch (choice)
            {
                case "1":
                    var registerEnterpriseCommand = new RegisterEnterpriseCommand(_specialist.SpecialistActions, Program.selectedBank);
                    _specialist._transactionInvoker.ExecuteCommand(registerEnterpriseCommand);
                    _specialist.Enterprise = registerEnterpriseCommand.Enterprise;
                    _specialist.IsEnterpriseRegistered = true;
                    _logDb.AddLog(_specialist.FullName, "Зарегистрировал предприятие");
                    break;
                case "2":
                    EnsureEnterpriseRegistered();
                    var applyForSalaryProjectCommand = new ApplyForSalaryProjectCommand(_specialist.SpecialistActions, _specialist.Enterprise);
                    _specialist._transactionInvoker.ExecuteCommand(applyForSalaryProjectCommand);
                    _logDb.AddLog(_specialist.FullName, "Подал заявку на зарплатный проект");
                    break;
                case "3":
                    EnsureEnterpriseRegistered();
                    var finalizeSalaryProjectCommand = new FinalizeSalaryProjectCommand(_specialist.SpecialistActions, _specialist.Enterprise);
                    _specialist._transactionInvoker.ExecuteCommand(finalizeSalaryProjectCommand);
                    _logDb.AddLog(_specialist.FullName, "Оформил зарплатный проект");
                    break;
                case "4":
                    EnsureEnterpriseRegistered();
                    Console.Write("Введите ФИО сотрудника: ");
                    string employeeName = Console.ReadLine();
                    Console.Write("Введите сумму перевода: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                    {
                        var transferFundsToEmployeeCommand = new TransferFundsToEmployeeCommand(_specialist.SpecialistActions, employeeName, amount);
                        _specialist._transactionInvoker.ExecuteCommand(transferFundsToEmployeeCommand);
                        _logDb.AddLog(_specialist.FullName, "Перевел средства сотруднику");
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
                        var depositToEnterpriseAccountCommand = new DepositToEnterpriseAccountCommand(_specialist.SpecialistActions, _specialist.Enterprise, depositAmount);
                        _specialist._transactionInvoker.ExecuteCommand(depositToEnterpriseAccountCommand);
                        _logDb.AddLog(_specialist.FullName, "Пополнил счет предприятия");
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод суммы.");
                    }
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Некорректный ввод.");
                    break;
            }
        }

        private void EnsureEnterpriseRegistered()
        {
            if (!_specialist.IsEnterpriseRegistered)
            {
                Console.WriteLine("Пожалуйста, зарегистрируйте предприятие.");
                var registerEnterpriseCommand = new RegisterEnterpriseCommand(_specialist.SpecialistActions, Program.selectedBank);
                _specialist._transactionInvoker.ExecuteCommand(registerEnterpriseCommand);
                _specialist.Enterprise = registerEnterpriseCommand.Enterprise;
                _specialist.IsEnterpriseRegistered = true;
            }
        }
    }
}
