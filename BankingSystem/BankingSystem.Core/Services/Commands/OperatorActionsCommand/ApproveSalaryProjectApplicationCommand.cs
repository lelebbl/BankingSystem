using BankingSystem.BankingSystem.Core.Entities.Operations;
using BankingSystem.BankingSystem.Core.Services.Commands;
using BankingSystem.BankingSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Services.Commands.OperatorActionsCommand
{
    public class ApproveSalaryProjectApplicationCommand : ICommand
    {
        public string GetActionName()
        {
            return "Одобрение заявки на зарплатный проект";
        }

        private readonly List<SalaryProjectApplication> _applications;
        private readonly int _index;
        private SalaryProjectApplication _application;

        public ApproveSalaryProjectApplicationCommand(List<SalaryProjectApplication> applications, int index)
        {
            _applications = applications;
            _index = index;
        }

        public void Execute()
        {
            _application = _applications[_index];
            _application.Approve();
            Console.WriteLine($"Заявка на зарплатный проект для {_application.Enterprise.LegalName} одобрена.");

            TransactionDatabase transactionDb = new TransactionDatabase();
            transactionDb.AddTransaction("Оператор", "Одобрение заявки на зарплатный проект", 0, _application.Enterprise.LegalName);
        }

        public void Undo()
        {
            _application.Reject();
            Console.WriteLine($"Одобрение заявки на зарплатный проект для {_application.Enterprise.LegalName} отменено.");

            TransactionDatabase transactionDb = new TransactionDatabase();
            transactionDb.AddTransaction("Оператор", "Отмена одобрения заявки на зарплатный проект", 0, _application.Enterprise.LegalName);
        }
    }
}
