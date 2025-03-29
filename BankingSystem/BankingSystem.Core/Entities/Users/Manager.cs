using BankingSystem.BankingSystem.Core.Entities.Operations;
using BankingSystem.BankingSystem.Core.Enums;
using BankingSystem.BankingSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.BankingSystem.Data;
using BankingSystem.BankingSystem.Core.Services.Commands.ManagerActionsCommand;
using BankingSystem.BankingSystem.Core.Services.Commands;
using BankingSystem.BankingSystem.Core.UI;

namespace BankingSystem.BankingSystem.Core.Entities.Users
{
    public class Manager : User
    {
        private readonly AuthService _authService;
        internal CommandInvoker _transactionInvoker;

        public Manager(string fullName, string passportNumber, string idNumber, string phone, string email, string password, AuthService authService)
            : base(fullName, passportNumber, idNumber, phone, email, password, UserRole.Manager)
        {
            _authService = authService;
            _transactionInvoker = new CommandInvoker();
            this.UserUi = new ManagerUi(this);
        }

        public AuthService AuthService
        {
            get { return _authService; }
        }

        public CommandInvoker TransactionInvoker
        {
            get { return _transactionInvoker; }
        }
    }
}

