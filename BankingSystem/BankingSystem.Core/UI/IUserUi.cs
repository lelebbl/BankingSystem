using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.UI
{
    public interface IUserUi
    {
        void PerformRoleActions();
        void HandleAction(string choice);
    }
}
