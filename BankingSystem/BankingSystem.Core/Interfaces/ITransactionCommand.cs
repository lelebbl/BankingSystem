using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Interfaces
{
    public interface ITransactionCommand
    {
        void Execute();
        void Undo();
    }
}
