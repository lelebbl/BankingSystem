using BankingSystem.BankingSystem.Core.Services.Commands.AccountManagerCommand;
using BankingSystem.BankingSystem.Core.Services.Commands.OperationsCommand;
using BankingSystem.BankingSystem.Core.Enums;
using BankingSystem.BankingSystem.Core.Entities.Operations;
using BankingSystem.BankingSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.BankingSystem.Core.Services.Commands;
using BankingSystem.BankingSystem.Core.Services;
using BankingSystem.BankingSystem.Core.UI;

namespace BankingSystem.BankingSystem.Core.Entities.Users
{
    public class Client : User
    {
        public List<Account> accounts = new List<Account>();
        public List<Application> applications { get; } = new List<Application>();
        public List<Transaction> transactions { get; } = new List<Transaction>();
        public CommandInvoker _transactionInvoker { get; private set; }
        public ClientUi ClientUi { get; private set; }

        public Client() 
        {
            this.UserUi = new ClientUi(this);
        }

        public Client(string fullName, string passportNumber, string idNumber, string phone, string email, string password, CommandInvoker transactionInvoker)
            : base(fullName, passportNumber, idNumber, phone, email, password, UserRole.Client) 
        {
            _transactionInvoker = transactionInvoker;
            this.UserUi = new ClientUi(this);
        }

        public void Deposit(string accountNumber, decimal amount)
        {
            var account = Account.FindAccount(accounts, accountNumber);
            if (account != null)
            {
                account.Deposit(amount);
                Console.WriteLine($"Счет {accountNumber} пополнен на {amount} руб.");
            }
            else
            {
                Console.WriteLine($"Счет {accountNumber} не найден.");
            }
        }

    }

}

