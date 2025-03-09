using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.BankingSystem.Core.Entities.Enterprises;
using BankingSystem.BankingSystem.Core.Actions;
using BankingSystem.BankingSystem.Core.Services;

namespace BankingSystem.BankingSystem.Core.Commands.SpecialistCommands
{
    public class DepositToEnterpriseAccountCommand : ICommand
    {
        public string GetActionName()
        {
            return "Пополнение счета предприятия";
        }

        private SpecialistActions _specialistActions;
        private ConcreteEnterprise _enterprise;
        private decimal _amount;

        public DepositToEnterpriseAccountCommand(SpecialistActions specialistActions, ConcreteEnterprise enterprise, decimal amount)
        {
            _specialistActions = specialistActions;
            _enterprise = enterprise;
            _amount = amount;
        }

        public void Execute()
        {
            _specialistActions.DepositToEnterpriseAccount(_enterprise, _amount);
        }

        public void Undo()
        {
            _specialistActions.UndoDepositToEnterpriseAccount(_enterprise, _amount);
            Console.WriteLine($"Пополнение счета предприятия {_enterprise.LegalName} на сумму {_amount} руб. отменено.");
        }
    }
}
