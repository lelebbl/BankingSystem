using BankingSystem.BankingSystem.Core.Entities.Accounts;
using BankingSystem.BankingSystem.Core.Entities.Users;
using BankingSystem.BankingSystem.Core.Entities.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.BankingSystem.Data;

namespace BankingSystem.BankingSystem.Core.Commands.OperationsCommand
{
    public class DepositAccountCommand : ICommand
    {
        public string GetActionName()
        {
            return "Создание вклада";
        }

        private Client _client;
        private string _accountNumber;
        private decimal _depositAmount;
        private decimal _interestRate;
        private int _termMonths;
        private DepositAccount _createdDepositAccount;
        private List<Account> _accounts;

        public DepositAccountCommand(Client client, List<Account> accounts)
        {
            _client = client;
            _accounts = accounts;
        }

        public void Execute()
        {
            if (_client.accounts.Count == 0)
            {
                Console.WriteLine("У вас нет открытых счетов. Сначала откройте счет.");
                AccountManager.OpenAccount(_client.accounts, _client._transactionInvoker);
                return;
            }

            Console.Write("Введите номер счета для вклада: ");
            _accountNumber = Console.ReadLine();
            var account = Account.FindAccount(_client.accounts, _accountNumber);

            if (account != null)
            {
                Console.Write("Введите сумму вклада: ");
                _depositAmount = decimal.Parse(Console.ReadLine());

                if (account.Balance >= _depositAmount)
                {
                    Console.Write("Введите срок (месяцы): ");
                    _termMonths = int.Parse(Console.ReadLine());

                    _interestRate = CalculateInterestRate(_termMonths);
                    account.Withdraw(_depositAmount);
                    _createdDepositAccount = new DepositAccount(_accountNumber, account.Balance, _depositAmount, _interestRate, _termMonths);
                    _client.accounts.Add(_createdDepositAccount);
                    Console.WriteLine($"Вклад создан на сумму {_depositAmount} с процентом {_interestRate}%. Текущий баланс: {account.Balance}");

                    TransactionDatabase transactionDb = new TransactionDatabase();
                    transactionDb.AddTransaction("Клиент", "Создание вклада", _depositAmount, _accountNumber);
                }
                else
                {
                    Console.WriteLine("Недостаточно средств на счете.");
                }
            }
            else
            {
                Console.WriteLine("Счет не найден.");
            }
        }

        public void Undo()
        {
            if (_createdDepositAccount != null)
            {
                _client.accounts.Remove(_createdDepositAccount);
                var account = Account.FindAccount(_client.accounts, _accountNumber);
                account.Deposit(_depositAmount);
                Console.WriteLine("Вклад отменен и средства возвращены на счет.");

                TransactionDatabase transactionDb = new TransactionDatabase();
                transactionDb.AddTransaction("Клиент", "Отмена вклада", _depositAmount, _accountNumber);
            }
        }

        private static decimal CalculateInterestRate(int termMonths)
        {
            if (termMonths <= 3) return 5;
            if (termMonths <= 6) return 10;
            if (termMonths <= 12) return 15;
            if (termMonths <= 24) return 20;
            return 25;
        }
    }
}
