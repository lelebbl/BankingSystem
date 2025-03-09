using BankingSystem.BankingSystem.Core.Actions;
using BankingSystem.BankingSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Commands.SpecialistCommands
{
    public class TransferFundsToEmployeeCommand : ICommand
    {
        public string GetActionName()
        {
            return "Перевод средств сотруднику предприятия";
        }

        private SpecialistActions _specialistActions;
        private string _employeeName;
        private decimal _amount;

        public TransferFundsToEmployeeCommand(SpecialistActions specialistActions, string employeeName, decimal amount)
        {
            _specialistActions = specialistActions;
            _employeeName = employeeName;
            _amount = amount;
        }

        public void Execute()
        {
            _specialistActions.TransferFundsToEmployee(_employeeName, _amount);
        }

        public void Undo()
        {
            _specialistActions.UndoTransferFundsToEmployee(_employeeName, _amount);
            Console.WriteLine($"Перевод средств сотруднику {_employeeName} отменен.");
        }
    }
}
