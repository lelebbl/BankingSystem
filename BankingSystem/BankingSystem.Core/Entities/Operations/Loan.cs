using BankingSystem.BankingSystem.Core.Entities.Accounts;
using BankingSystem.BankingSystem.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Entities.Operations
{
    public class Loan : Application
    {
        public Loan(Client applicant, decimal amount, int termMonths, string accountNumber)
            : base(applicant, amount, termMonths, accountNumber) { }

        public override string GetApplicationType() => "Кредит";

        public static void CreateLoanApplication(Client client)
        {
            Console.Write("Введите номер счета для зачисления кредита: ");
            string accountNumber = Console.ReadLine();
            var account = Account.FindAccount(client.accounts, accountNumber);

            if (account != null)
            {
                Console.Write("Введите сумму кредита: ");
                decimal creditAmount = decimal.Parse(Console.ReadLine());
                Console.Write("Введите срок (месяцы): ");
                int creditTerm = int.Parse(Console.ReadLine());
                client.applications.Add(new Loan(client, creditAmount, creditTerm, accountNumber));
                Console.WriteLine("Заявка на кредит отправлена.");
            }
            else
            {
                Console.WriteLine("Счет не найден.");
            }
        }

    }
}
