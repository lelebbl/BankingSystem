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
    public class InstallmentApplicationCommand : ICommand
    {
        private Client _client;
        private string _accountNumber;
        private decimal _installmentAmount;
        private int _installmentTerm;
        private Installment _createdInstallment;
        private List<Application> _applications;

        public InstallmentApplicationCommand(Client client, List<Application> applications)
        {
            _client = client;
            _applications = applications;
        }

        public void Execute()
        {
            Console.Write("Введите номер счета для зачисления рассрочки: ");
            _accountNumber = Console.ReadLine();
            var account = Account.FindAccount(_client.accounts, _accountNumber);

            if (account != null)
            {
                Console.Write("Введите сумму рассрочки: ");
                _installmentAmount = decimal.Parse(Console.ReadLine());
                Console.Write("Введите срок (месяцы): ");
                _installmentTerm = int.Parse(Console.ReadLine());
                _createdInstallment = new Installment(_client, _installmentAmount, _installmentTerm, _accountNumber);
                _applications.Add(_createdInstallment);
                Console.WriteLine("Заявка на рассрочку отправлена.");
            }
            else
            {
                Console.WriteLine("Счет не найден.");
            }
        }

        public void Undo()
        {
            if (_createdInstallment != null)
            {
                var account = Account.FindAccount(_client.accounts, _accountNumber);
                if (account != null)
                {
                    account.Withdraw(_installmentAmount);
                    Console.WriteLine($"Со счета списано {_installmentAmount} руб.");
                }
                _applications.Remove(_createdInstallment);
                Console.WriteLine("Заявка на рассрочку отменена.");
            }
            
        }
    }
}
