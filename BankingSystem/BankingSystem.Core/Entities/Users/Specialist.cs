using BankingSystem.BankingSystem.Core.Services.Commands.SpecialistCommands;
using BankingSystem.BankingSystem.Core.Enums;
using BankingSystem.BankingSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.BankingSystem.Data;
using BankingSystem.BankingSystem.Core.Services.Commands;
using BankingSystem.BankingSystem.Core.Entities.Operations;
using BankingSystem.BankingSystem.Core.UI;

namespace BankingSystem.BankingSystem.Core.Entities.Users
{
    public class Specialist : User
    {
        private EnterpriseService enterpriseService;
        private bool isEnterpriseRegistered = false;
        internal ConcreteEnterprise enterprise;
        private SpecialistActions specialistActions;
        internal CommandInvoker _transactionInvoker;

        public Specialist(string fullName, string passportNumber, string idNumber, string phone, string email, string password, CommandInvoker transactionInvoker)
            : base(fullName, passportNumber, idNumber, phone, email, password, UserRole.Specialist)
        {
            enterpriseService = new EnterpriseService();
            specialistActions = new SpecialistActions(enterpriseService, this, transactionInvoker);
            this._transactionInvoker = transactionInvoker;
            this.UserUi = new SpecialistUi(this);
        }

        public bool IsEnterpriseRegistered
        {
            get { return isEnterpriseRegistered; }
            set { isEnterpriseRegistered = value; }
        }

        public ConcreteEnterprise Enterprise
        {
            get { return enterprise; }
            set { enterprise = value; }
        }

        public SpecialistActions SpecialistActions
        {
            get { return specialistActions; }
        }
    }
}