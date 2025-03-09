using BankingSystem.BankingSystem.Core.Entities.Users;
using BankingSystem.BankingSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Commands.ManagerActionsCommand
{
    public class ApproveClientRegistrationsCommand : ICommand
    {
        public string GetActionName()
        {
            return "Одобрение регистрации клиента";
        }

        private readonly AuthService _authService;
        private List<User> _approvedUsers;

        public ApproveClientRegistrationsCommand(AuthService authService)
        {
            _authService = authService;
            _approvedUsers = new List<User>();
        }

        public void Execute()
        {
            var pendingUsers = _authService.GetPendingUsers();
            foreach (var user in pendingUsers)
            {
                _authService.ApproveUser(user);
                _approvedUsers.Add(user);
            }
        }

        public void Undo()
        {
            foreach (var user in _approvedUsers)
            {
                user.IsApproved = false;
                Console.WriteLine($"Отмена одобрения регистрации клиента: {user.FullName}");
            }
            _approvedUsers.Clear();
        }
    }
}
