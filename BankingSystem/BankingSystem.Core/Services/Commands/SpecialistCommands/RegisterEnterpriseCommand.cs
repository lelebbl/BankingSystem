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
    public class RegisterEnterpriseCommand : ICommand
    {
        public string GetActionName()
        {
            return "Регистрация предприятия";
        }

        private SpecialistActions _specialistActions;
        private Bank _bank;
        private ConcreteEnterprise _enterprise;

        public RegisterEnterpriseCommand(SpecialistActions specialistActions, Bank bank)
        {
            _specialistActions = specialistActions;
            _bank = bank;
        }

        public ConcreteEnterprise Enterprise => _enterprise;

        public void Execute()
        {
            _enterprise = _specialistActions.RegisterEnterprise(_bank);
        }

        public void Undo()
        {
            if (_enterprise != null)
            {
                _specialistActions.UnregisterEnterprise(_enterprise);
                Console.WriteLine($"Регистрация предприятия {_enterprise.LegalName} отменена.");
            }
        }
    }
}
