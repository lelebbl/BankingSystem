using BankingSystem.BankingSystem.Core.Entities.Accounts;
using BankingSystem.BankingSystem.Core.Entities.Operations;
using BankingSystem.BankingSystem.Core.Enums;
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

        public Client(string fullName, string passportNumber, string idNumber, string phone, string email, string password)
            : base(fullName, passportNumber, idNumber, phone, email, password, UserRole.Client) { }

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
            switch (choice)
            {
                case "1":
                    AccountManager.OpenAccount(accounts);
                    break;
                case "2":
                    Account.DisplayAccounts(this);
                    break;
                case "3":
                    AccountManager.DepositToAccount(accounts);
                    break;
                case "4":
                    AccountManager.WithdrawFromAccount(accounts);
                    break;
                case "5":
                    AccountManager.CloseAccount(accounts);
                    break;
                case "6":
                    Loan.CreateLoanApplication(this);
                    break;
                case "7":
                    Installment.CreateInstallmentApplication(this);
                    break;
                case "8":
                    DepositAccount.CreateDeposit(this);
                    break;
                case "9":
                    Transaction.CreateTransaction(this);
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

