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

        //public void Execute()
        //{
        //    var pendingUsers = _authService.GetPendingUsers();
        //    foreach (var user in pendingUsers)
        //    {
        //        _authService.ApproveUser(user);
        //        _approvedUsers.Add(user);
        //    }
        //}

        public void Execute()
        {
            var pendingUsers = _authService.GetPendingUsers();
            if (pendingUsers.Count == 0)
            {
                Console.WriteLine("Нет клиентов, ожидающих одобрения.");
                return;
            }

            Console.WriteLine("Клиенты на одобрение:");
            for (int i = 0; i < pendingUsers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {pendingUsers[i].FullName} ({pendingUsers[i].Email})");
            }

            Console.Write("Введите номер клиента для одобрения (или 'all' для одобрения всех): ");
            string input = Console.ReadLine();

            if (input.ToLower() == "all")
            {
                foreach (var user in pendingUsers)
                {
                    _authService.ApproveUser(user);
                    _approvedUsers.Add(user);
                }
                Console.WriteLine("Все клиенты одобрены!");
            }
            else if (int.TryParse(input, out int userIndex) && userIndex > 0 && userIndex <= pendingUsers.Count)
            {
                var selectedUser = pendingUsers[userIndex - 1];
                _authService.ApproveUser(selectedUser);
                _approvedUsers.Add(selectedUser);
                Console.WriteLine($"Клиент {selectedUser.FullName} одобрен!");
            }
            else
            {
                Console.WriteLine("Некорректный ввод.");
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
