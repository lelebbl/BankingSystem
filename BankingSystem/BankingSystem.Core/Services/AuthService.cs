using BankingSystem.BankingSystem.Core.Commands;
using BankingSystem.BankingSystem.Core.Entities;
using BankingSystem.BankingSystem.Core.Entities.Users;
using BankingSystem.BankingSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Services
{
    public class AuthService
    {
        private readonly List<User> _users = new List<User>();
        private readonly CommandInvoker _transactionInvoker;

        public AuthService(CommandInvoker transactionInvoker)
        {
            _transactionInvoker = transactionInvoker;

            _users.Add(new Manager("Анна Менеджер", "AB1234567", "123456789", "+375291234567", "1", "1", this) { IsApproved = true });
            _users.Add(new Administrator("Петр Администратор", "CD7654321", "987654321", "+375291112233", "4", "4", _transactionInvoker) { IsApproved = true });
            _users.Add(new Client("Ирина Клиент", "MP9876543", "123456789", "+375444567890", "2", "2", _transactionInvoker) { IsApproved = true });
            _users.Add(new Specialist("Мария Клиент", "MP9876543", "123456789", "+375444567890", "3", "3", _transactionInvoker) { IsApproved = true });
            _users.Add(new Operator("Максим Оператор", "MP9876543", "123456789", "+375444567890", "5", "5", _transactionInvoker) { IsApproved = true });
        }

        public User Login(string email, string password)
        {
            return _users.FirstOrDefault(user => user.Email == email && user.Password == password);
        }

        public User Register(string fullName, string passport, string id, string phone, string email, string password, UserRole role)
        {
            User newUser = role switch
            {
                UserRole.Client => new Client(fullName, passport, id, phone, email, password, _transactionInvoker),
                UserRole.Operator => new Operator(fullName, passport, id, phone, email, password, _transactionInvoker),
                UserRole.Manager => new Manager(fullName, passport, id, phone, email, password, this),
                UserRole.Specialist => new Specialist(fullName, passport, id, phone, email, password, _transactionInvoker),
                UserRole.Administrator => new Administrator(fullName, passport, id, phone, email, password, _transactionInvoker),
                _ => throw new ArgumentException("Неверная роль")
            };

            _users.Add(newUser);
            Console.WriteLine($"Пользователь {newUser.FullName} зарегистрирован как {newUser.Role}.");

            return newUser;
        }

        public List<User> GetPendingUsers()
        {
            return _users.Where(u => !u.IsApproved && u.Role == UserRole.Client).ToList();
        }

        public void ApproveUser(User user)
        {
            user.IsApproved = true;
            Console.WriteLine($"Менеджер одобрил регистрацию клиента: {user.FullName}");
        }

        public List<Client> GetAllClients()
        {
            return _users.OfType<Client>().ToList();
        }
    }
}
