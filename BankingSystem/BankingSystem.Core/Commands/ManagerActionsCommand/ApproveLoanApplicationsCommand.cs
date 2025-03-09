using BankingSystem.BankingSystem.Core.Actions;
using BankingSystem.BankingSystem.Core.Entities.Accounts;
using BankingSystem.BankingSystem.Core.Entities.Operations;
using BankingSystem.BankingSystem.Core.Services;
using BankingSystem.BankingSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Commands.ManagerActionsCommand
{
    public class ApproveLoanApplicationsCommand : ICommand
    {
        public string GetActionName()
        {
            return "Одобрение заявки на кредит";
        }

        private readonly AuthService _authService;
        private List<Loan> _approvedLoans;

        public ApproveLoanApplicationsCommand(AuthService authService)
        {
            _authService = authService;
            _approvedLoans = new List<Loan>();
        }

        public void Execute()
        {
            var pendingLoans = ManagerActions.GetPendingApplications<Loan>(_authService);
            foreach (var loan in pendingLoans)
            {
                loan.Approve();
                _approvedLoans.Add(loan);

                TransactionDatabase transactionDb = new TransactionDatabase();
                transactionDb.AddTransaction("Менеджер", "Одобрение кредита", loan.Amount, loan.AccountNumber);
            }
        }

        public void Undo()
        {
            foreach (var loan in _approvedLoans)
            {
                loan.RevokeApproval();
                Console.WriteLine($"Отмена одобрения кредита для клиента: {loan.Applicant.FullName}");

                TransactionDatabase transactionDb = new TransactionDatabase();
                transactionDb.AddTransaction("Менеджер", "Отмена одобрения кредита", loan.Amount, loan.AccountNumber);
            }
            _approvedLoans.Clear();
        }
    }
}
