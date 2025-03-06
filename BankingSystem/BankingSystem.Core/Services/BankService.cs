using BankingSystem.BankingSystem.Core.Entities.Banks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Services
{
    public class BankService
    {
        public List<Bank> Banks { get; } = new List<Bank>();

        public BankService()
        {
            Banks.Add(new SberBank());
            Banks.Add(new AlfaBank());
            Banks.Add(new TinkoffBank());
        }

        public Bank GetBankByIndex(int index)
        {
            return (index >= 0 && index < Banks.Count) ? Banks[index] : null;
        }
    }
}
