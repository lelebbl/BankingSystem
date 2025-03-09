using BankingSystem.BankingSystem.Core.Actions;
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
    public class ApproveInstallmentApplicationsCommand : ICommand
    {
        private readonly AuthService _authService;
        private List<Installment> _approvedInstallments;

        public ApproveInstallmentApplicationsCommand(AuthService authService)
        {
            _authService = authService;
            _approvedInstallments = new List<Installment>();
        }

        public void Execute()
        {
            var pendingInstallments = ManagerActions.GetPendingApplications<Installment>(_authService);
            foreach (var installment in pendingInstallments)
            {
                installment.Approve();
                _approvedInstallments.Add(installment);

                TransactionDatabase transactionDb = new TransactionDatabase();
                transactionDb.AddTransaction("Менеджер", "Одобрение рассрочки", installment.Amount, installment.AccountNumber);
            }
        }

        public void Undo()
        {
            foreach (var installment in _approvedInstallments)
            {
                installment.RevokeApproval();
                Console.WriteLine($"Отмена одобрения рассрочки для клиента: {installment.Applicant.FullName}");
            }
            _approvedInstallments.Clear();
        }
    }
}
