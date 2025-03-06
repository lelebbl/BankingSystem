using BankingSystem.BankingSystem.Core.Entities.Accounts;
using BankingSystem.BankingSystem.Core.Entities.Operations;
using BankingSystem.BankingSystem.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Commands.OperationsCommand
{
    public class LoanApplicationCommand : ICommand
    {
        private Client _client;
        private string _accountNumber;
        private decimal _creditAmount;
        private int _creditTerm;
        private Loan _createdLoan;
        private List<Application> _applications;

        public LoanApplicationCommand(Client client, List<Application> applications)
        {
            _client = client;
            _applications = applications;
        }

        public void Execute()
        {
            Console.Write("Введите номер счета для зачисления кредита: ");
            _accountNumber = Console.ReadLine();
            var account = Account.FindAccount(_client.accounts, _accountNumber);

            if (account != null)
            {
                Console.Write("Введите сумму кредита: ");
                _creditAmount = decimal.Parse(Console.ReadLine());
                Console.Write("Введите срок (месяцы): ");
                _creditTerm = int.Parse(Console.ReadLine());
                _createdLoan = new Loan(_client, _creditAmount, _creditTerm, _accountNumber);
                _applications.Add(_createdLoan);
                Console.WriteLine("Заявка на кредит отправлена.");
            }
            else
            {
                Console.WriteLine("Счет не найден.");
            }
        }

        public void Undo()
        {
            if (_createdLoan != null)
            {
                var account = Account.FindAccount(_client.accounts, _accountNumber);
                if (account != null)
                {
                    account.Withdraw(_creditAmount);
                    Console.WriteLine($"Со счета списано {_creditAmount} руб.");
                }
                _applications.Remove(_createdLoan);
                Console.WriteLine("Заявка на кредит отменена.");
            }
            
        }
    }
}
