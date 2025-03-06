using BankingSystem.BankingSystem.Core.Entities.Enterprises;
using BankingSystem.BankingSystem.Core.Entities.Operations;
using BankingSystem.BankingSystem.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Entities.Banks
{
    public abstract class Bank
    {
        public string Name { get; }
        public string BIK { get; }
        public List<Enterprise> Enterprises { get; } = new List<Enterprise>();
        public List<Client> Clients { get; } = new List<Client>();
        private List<SalaryProjectApplication> salaryProjectApplications = new List<SalaryProjectApplication>();

        protected Bank(string name, string bik)
        {
            Name = name;
            BIK = bik;
        }

        public abstract void RegisterEnterprise(Enterprise enterprise);
        public abstract void AddClient(Client client);

        public void RegisterClient(Client client)
        {
            Clients.Add(client);
        }

        public Enterprise FindEnterpriseByName(string legalName)
        {
            return Enterprises.FirstOrDefault(e => e.LegalName == legalName);
        }

        public void AddSalaryProjectApplication(SalaryProjectApplication application)
        {
            salaryProjectApplications.Add(application);
        }

        public List<SalaryProjectApplication> GetAllSalaryProjectApplications()
        {
            return salaryProjectApplications.ToList();
        }

        public SalaryProjectApplication GetSalaryProjectApplication(string legalName)
        {
            return salaryProjectApplications.FirstOrDefault(app => app.Enterprise.LegalName == legalName);
        }
    }

}
