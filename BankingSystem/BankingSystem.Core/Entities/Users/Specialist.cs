using BankingSystem.BankingSystem.Core.Commands;
using BankingSystem.BankingSystem.Core.Entities.Banks;
using BankingSystem.BankingSystem.Core.Entities.Enterprises;
using BankingSystem.BankingSystem.Core.Enums;
using BankingSystem.BankingSystem.Core.Services;
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

        public Specialist(string fullName, string passportNumber, string idNumber, string phone, string email, string password, CommandInvoker transactionInvoker)
            : base(fullName, passportNumber, idNumber, phone, email, password, UserRole.Specialist)
        {
            enterpriseService = new EnterpriseService();
            specialistActions = new SpecialistActions(enterpriseService, this, transactionInvoker);
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
                    enterprise = specialistActions.RegisterEnterprise(Program.selectedBank);
                    isEnterpriseRegistered = true;
                    break;
                case "2":
                    EnsureEnterpriseRegistered();
                    specialistActions.ApplyForSalaryProject(enterprise);
                    break;
                case "3":
                    EnsureEnterpriseRegistered();
                    specialistActions.FinalizeSalaryProject(enterprise);
                    break;
                case "4":
                    EnsureEnterpriseRegistered();
                    specialistActions.TransferFundsToEmployee();
                    break;
                case "5":
                    EnsureEnterpriseRegistered();
                    specialistActions.DepositToEnterpriseAccount(enterprise);
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
                enterprise = specialistActions.RegisterEnterprise(Program.selectedBank);
                isEnterpriseRegistered = true;
            }
        }
    }
}