using BankingSystem.BankingSystem.Core.Entities.Operations;
using BankingSystem.BankingSystem.Core.Services;
using BankingSystem.BankingSystem.Core.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Services.Commands.SpecialistCommands
{
    public class FinalizeSalaryProjectCommand : ICommand
    {
        public string GetActionName()
        {
            return "Оформление зарплатного проекта";
        }

        private SpecialistActions _specialistActions;
        private ConcreteEnterprise _enterprise;

        public FinalizeSalaryProjectCommand(SpecialistActions specialistActions, ConcreteEnterprise enterprise)
        {
            _specialistActions = specialistActions;
            _enterprise = enterprise;
        }

        public void Execute()
        {
            _specialistActions.FinalizeSalaryProject(_enterprise);
        }

        public void Undo()
        {
            _specialistActions.CancelFinalizeSalaryProject(_enterprise);
            Console.WriteLine("Оформление зарплатного проекта отменено.");
        }
    }
}
