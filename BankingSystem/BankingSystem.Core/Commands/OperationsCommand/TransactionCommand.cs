using BankingSystem.BankingSystem.Core.Entities.Accounts;
using BankingSystem.BankingSystem.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.BankingSystem.Core.Entities.Operations;
using BankingSystem.BankingSystem.Data;

namespace BankingSystem.BankingSystem.Core.Commands.OperationsCommand
{
    public class TransactionCommand : ICommand
    {
        public string GetActionName()
        {
            return "Перевод средств"; 
        }

        private Client _client;
        private string _fromAccountNumber;
        private string _toAccountNumber;
        private decimal _amount;
        private Transaction _createdTransaction;

        public TransactionCommand(Client client)
        {
            _client = client;
        }

        public void Execute()
        {
            if (_client.accounts.Count == 0)
            {
                Console.WriteLine("У вас нет открытых счетов. Сначала откройте счет.");
                AccountManager.OpenAccount(_client.accounts, _client._transactionInvoker);
                return;
            }

            Console.Write("Введите номер счета, с которого хотите перевести средства: ");
            _fromAccountNumber = Console.ReadLine();
            var fromAccount = Account.FindAccount(_client.accounts, _fromAccountNumber);

            if (fromAccount == null)
            {
                Console.WriteLine("Счет не найден.");
                return;
            }

            Console.Write("Введите номер счета, на который хотите перевести средства: ");
            _toAccountNumber = Console.ReadLine();
            var toAccount = Account.FindAccount(_client.accounts, _toAccountNumber);

            if (toAccount == null)
            {
                Console.WriteLine("Счет не найден.");
                return;
            }

            Console.Write("Введите сумму перевода: ");
            _amount = decimal.Parse(Console.ReadLine());

            if (fromAccount.Balance < _amount)
            {
                Console.WriteLine("Недостаточно средств.");
                return;
            }

            fromAccount.Withdraw(_amount);
            toAccount.Deposit(_amount);

            _createdTransaction = new Transaction(_fromAccountNumber, _toAccountNumber, _amount);
            _client.transactions.Add(_createdTransaction);
            Console.WriteLine($"Перевод {_amount} руб. выполнен успешно с {_fromAccountNumber} на {_toAccountNumber}. Текущий баланс: {fromAccount.Balance}");

            TransactionDatabase transactionDb = new TransactionDatabase();
            transactionDb.AddTransaction("Клиент", "Перевод средств со счета", _amount, _fromAccountNumber);
            transactionDb.AddTransaction("Клиент", "Перевод средств на счет", _amount, _toAccountNumber);
        }

        public void Undo()
        {
            if (_createdTransaction != null)
            {
                var fromAccount = Account.FindAccount(_client.accounts, _fromAccountNumber);
                var toAccount = Account.FindAccount(_client.accounts, _toAccountNumber);

                if (fromAccount != null && toAccount != null)
                {
                    toAccount.Withdraw(_amount);
                    fromAccount.Deposit(_amount);
                    _client.transactions.Remove(_createdTransaction);
                    Console.WriteLine("Перевод отменен и средства возвращены на исходный счет.");

                    TransactionDatabase transactionDb = new TransactionDatabase();
                    transactionDb.AddTransaction("Клиент", "Отмена перевода средств со счета", _amount, _fromAccountNumber);
                    transactionDb.AddTransaction("Клиент", "Отмена перевода средств на счет", _amount, _toAccountNumber);
                }
            }
        }
    }
}
