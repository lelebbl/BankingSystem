using BankingSystem.BankingSystem.Core.Enums;
using BankingSystem.BankingSystem.Core.Services;
using BankingSystem.BankingSystem.Core.Services.Commands;
using BankingSystem.BankingSystem.Core.UI;
using BankingSystem.BankingSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Entities.Users
{
    public class Administrator : User
    {
        private CommandInvoker _transactionInvoker;
        private LogDatabase logDb;

        public Administrator(string fullName, string passportNumber, string idNumber, string phone, string email, string password, CommandInvoker transactionInvoker)
            : base(fullName, passportNumber, idNumber, phone, email, password, UserRole.Administrator)
        {
            _transactionInvoker = transactionInvoker;
            logDb = new LogDatabase();
            this.UserUi = new AdministratorUi(this);
        }

        public CommandInvoker TransactionInvoker
        {
            get { return _transactionInvoker; }
        }
    }
}

