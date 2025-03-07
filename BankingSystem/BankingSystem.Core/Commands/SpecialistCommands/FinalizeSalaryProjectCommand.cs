using BankingSystem.BankingSystem.Core.Entities.Enterprises;
using BankingSystem.BankingSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Commands.SpecialistCommands
{
    public class FinalizeSalaryProjectCommand : ICommand
    {
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
