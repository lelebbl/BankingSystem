using BankingSystem.BankingSystem.Core.Entities.Operations;
using BankingSystem.BankingSystem.Core.Entities.Users;
using BankingSystem.BankingSystem.Core.Services.Commands.OperationsCommand;
using BankingSystem.BankingSystem.Core.Services;
using BankingSystem.BankingSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.UI
{
    public class ClientUi : IUserUi
    {
        private Client _client;
        private LogDatabase _logDb;

        public ClientUi(Client client)
        {
            _client = client;
            _logDb = new LogDatabase();
        }

        public void PerformRoleActions()
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

        public void HandleAction(string choice)
        {
            switch (choice)
            {
                case "1":
                    AccountManager.OpenAccount(_client.accounts, _client._transactionInvoker);
                    _logDb.AddLog(_client.FullName, "Открыл новый счет");
                    break;
                case "2":
                    Account.DisplayAccounts(_client);
                    _logDb.AddLog(_client.FullName, "Просмотрел все счета");
                    break;
                case "3":
                    AccountManager.DepositToAccount(_client.accounts, _client._transactionInvoker);
                    _logDb.AddLog(_client.FullName, "Пополнил счет");
                    break;
                case "4":
                    AccountManager.WithdrawFromAccount(_client.accounts, _client._transactionInvoker);
                    _logDb.AddLog(_client.FullName, "Снял средства со счета");
                    break;
                case "5":
                    AccountManager.CloseAccount(_client.accounts, _client._transactionInvoker);
                    _logDb.AddLog(_client.FullName, "Закрыл счет");
                    break;
                case "6":
                    var loanApplicationCommand = new LoanApplicationCommand(_client, _client.applications);
                    _client._transactionInvoker.ExecuteCommand(loanApplicationCommand);
                    _logDb.AddLog(_client.FullName, "Подал заявку на кредит");
                    break;
                case "7":
                    var installmentApplicationCommand = new InstallmentApplicationCommand(_client, _client.applications);
                    _client._transactionInvoker.ExecuteCommand(installmentApplicationCommand);
                    _logDb.AddLog(_client.FullName, "Подал заявку на рассрочку");
                    break;
                case "8":
                    var depositAccountCommand = new DepositAccountCommand(_client, _client.accounts);
                    _client._transactionInvoker.ExecuteCommand(depositAccountCommand);
                    _logDb.AddLog(_client.FullName, "Создал вклад");
                    break;
                case "9":
                    var transactionCommand = new TransactionCommand(_client);
                    _client._transactionInvoker.ExecuteCommand(transactionCommand);
                    _logDb.AddLog(_client.FullName, "Перевел средства");
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                    break;
            }
        }
    }
}
