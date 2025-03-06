using BankingSystem.BankingSystem.Core.Entities.Enterprises;
using BankingSystem.BankingSystem.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Entities.Banks
{
    public class AlfaBank : Bank
    {
        public AlfaBank() : base("Альфа-Банк", "044525593") { }

        public override void RegisterEnterprise(Enterprise enterprise)
        {
            Enterprises.Add(enterprise);
            Console.WriteLine($"Предприятие {enterprise.LegalName} зарегистрировано в {Name}.");
        }

        public override void AddClient(Client client)
        {
            Clients.Add(client);
            Console.WriteLine($"Клиент {client.FullName} зарегистрирован в {Name}.");
        }
    }
}
