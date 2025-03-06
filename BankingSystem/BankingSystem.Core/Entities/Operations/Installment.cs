using BankingSystem.BankingSystem.Core.Entities.Accounts;
using BankingSystem.BankingSystem.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Entities.Operations
{

    public class Installment : Application
    {
        public Installment(Client applicant, decimal amount, int termMonths, string accountNumber)
            : base(applicant, amount, termMonths, accountNumber) { }

        public override string GetApplicationType() => "Рассрочка";

        public static void CreateInstallmentApplication(Client client)
        {
            Console.Write("Введите номер счета для зачисления рассрочки: ");
            string accountNumber = Console.ReadLine();
            var account = Account.FindAccount(client.accounts, accountNumber);

            if (account != null)
            {
                Console.Write("Введите сумму рассрочки: ");
                decimal installmentAmount = decimal.Parse(Console.ReadLine());
                Console.Write("Введите срок (месяцы): ");
                int installmentTerm = int.Parse(Console.ReadLine());
                client.applications.Add(new Installment(client, installmentAmount, installmentTerm, accountNumber));
                Console.WriteLine("Заявка на рассрочку отправлена.");
            }
            else
            {
                Console.WriteLine("Счет не найден.");
            }
        }
    }
}
