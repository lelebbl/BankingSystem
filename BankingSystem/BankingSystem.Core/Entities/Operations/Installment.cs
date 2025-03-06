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
    }
}
