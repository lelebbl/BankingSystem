using BankingSystem.BankingSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Entities.Users
{
    public abstract class User
    {
        public string FullName { get; set; }
        public string PassportNumber { get; set; }
        public string IDNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; protected set; }
        public bool IsApproved { get; set; }   // Требует одобрения

        public User() { }

        protected User(string fullName, string passportNumber, string idNumber, string phone, string email, string password, UserRole role)
        {
            FullName = fullName;
            PassportNumber = passportNumber;
            IDNumber = idNumber;
            Phone = phone;
            Email = email;
            Password = password;
            Role = role;
            IsApproved = role != UserRole.Client;
        }

        public abstract void PerformRoleActions();
        public abstract void HandleAction(string choice);
    }
}
