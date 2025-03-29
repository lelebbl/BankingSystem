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
    public class ApplyForSalaryProjectCommand : ICommand
    {
        public string GetActionName()
        {
            return "Заявка на зарплатный проект";
        }

        private SpecialistActions _specialistActions;
        private ConcreteEnterprise _enterprise;

        public ApplyForSalaryProjectCommand(SpecialistActions specialistActions, ConcreteEnterprise enterprise)
        {
            _specialistActions = specialistActions;
            _enterprise = enterprise;
        }

        public void Execute()
        {
            _specialistActions.ApplyForSalaryProject(_enterprise);
        }

        public void Undo()
        {
            _specialistActions.CancelSalaryProjectApplication(_enterprise);
            Console.WriteLine("Заявка на зарплатный проект отменена.");
        }
    }
}
