using BankingSystem.BankingSystem.Core.Entities.Banks;
using BankingSystem.BankingSystem.Core.Entities.Enterprises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Services
{
    public class EnterpriseService
    {
        public List<Enterprise> Enterprises { get; } = new List<Enterprise>();
        private EnterpriseService enterpriseService;

        public EnterpriseService()
        {
            this.enterpriseService = this;
        }

        public void RegisterEnterprise(Enterprise enterprise)
        {
            Enterprises.Add(enterprise);
            enterprise.Bank.RegisterEnterprise(enterprise);
        }

        public Enterprise CreateEnterprise(string type, string legalName, string unp, string legalAddress, Bank bank, string bik)
        {
            return new ConcreteEnterprise(type, legalName, unp, legalAddress, bank, bik, enterpriseService);
        }

        public Enterprise FindEnterpriseByName(string name)
        {
            return Enterprises.FirstOrDefault(e => e.LegalName == name);
        }
    }
}
