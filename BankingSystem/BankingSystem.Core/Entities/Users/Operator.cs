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
    public class Operator : User
    {
        private CommandInvoker _transactionInvoker;
        private bool hasUndoBeenPerformed = false;

        public Operator(string fullName, string passportNumber, string idNumber, string phone, string email, string password, CommandInvoker transactionInvoker)
            : base(fullName, passportNumber, idNumber, phone, email, password, UserRole.Operator)
        {
            _transactionInvoker = transactionInvoker;
            this.UserUi = new OperatorUi(this);
        }

        public CommandInvoker TransactionInvoker
        {
            get { return _transactionInvoker; }
        }
    }
}
