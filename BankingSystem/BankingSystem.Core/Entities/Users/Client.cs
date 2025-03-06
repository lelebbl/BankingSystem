using BankingSystem.BankingSystem.Core.Commands;
using BankingSystem.BankingSystem.Core.Commands.AccountManagerCommand;
using BankingSystem.BankingSystem.Core.Commands.OperationsCommand;
using BankingSystem.BankingSystem.Core.Entities.Accounts;
using BankingSystem.BankingSystem.Core.Entities.Operations;
using BankingSystem.BankingSystem.Core.Enums;
using BankingSystem.BankingSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Entities.Users
{
    public class Client : User
    {
        public List<Account> accounts = new List<Account>();
        public List<Application> applications { get; } = new List<Application>();
        public List<Transaction> transactions { get; } = new List<Transaction>();
        public CommandInvoker _transactionInvoker { get; private set; }

        public Client(string fullName, string passportNumber, string idNumber, string phone, string email, string password, CommandInvoker transactionInvoker)
            : base(fullName, passportNumber, idNumber, phone, email, password, UserRole.Client) 
        {
            _transactionInvoker = transactionInvoker;
        }

        public override void PerformRoleActions()
        {
            Console.WriteLine("1 - Открыть счет");
            Console.WriteLine("2 - Просмотреть все счета");
            Console.WriteLine("3 - Пополнить счет");
            Console.WriteLine("4 - Снять средства со счета");
            Console.WriteLine("5 - Закрыть счет");
            Console.WriteLine("6 - Подать заявку на кредит");
            Console.WriteLine("7 - Подать заявку на рассрочку");
            Console.WriteLine("8 - Создать вклад");
            Console.WriteLine("9 - Перевести средства");
            Console.WriteLine("0 - Выйти в главное меню");
        }

        public override void HandleAction(string choice)
        {
            LogDatabase logDb = new LogDatabase();

            switch (choice)
            {
                case "1":
                    AccountManager.OpenAccount(accounts, _transactionInvoker);
                    logDb.AddLog(FullName, "Открыл новый счет");
                    break;
                case "2":
                    Account.DisplayAccounts(this);
                    break;
                case "3":
                    AccountManager.DepositToAccount(accounts, _transactionInvoker);
                    logDb.AddLog(FullName, "Пополнил счет");
                    break;
                case "4":
                    AccountManager.WithdrawFromAccount(accounts, _transactionInvoker);
                    break;
                case "5":
                    AccountManager.CloseAccount(accounts, _transactionInvoker);
                    break;
                case "6":
                    var loanApplicationCommand = new LoanApplicationCommand(this, applications);
                    _transactionInvoker.ExecuteCommand(loanApplicationCommand);
                    break;
                case "7":
                    var installmentApplicationCommand = new InstallmentApplicationCommand(this, applications);
                    _transactionInvoker.ExecuteCommand(installmentApplicationCommand);
                    break;
                case "8":
                    var depositAccountCommand = new DepositAccountCommand(this, accounts);
                    _transactionInvoker.ExecuteCommand(depositAccountCommand);
                    break;
                case "9":
                    var transactionCommand = new TransactionCommand(this);
                    _transactionInvoker.ExecuteCommand(transactionCommand);
                    logDb.AddLog(FullName, "Перевел средства");
                    break;
                case "0":

                    break;
                default:
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                    break;
            }
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

