using BankingSystem.BankingSystem.Core.Actions;
using BankingSystem.BankingSystem.Core.Commands;
using BankingSystem.BankingSystem.Core.Enums;
using BankingSystem.BankingSystem.Core.Services;
using BankingSystem.BankingSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Entities.Users
{
    public class Operator : User
    {
        private CommandInvoker _transactionInvoker;
        private bool hasUndoBeenPerformed = false;

        public Operator(string fullName, string passportNumber, string idNumber, string phone, string email, string password, CommandInvoker transactionInvoker)
            : base(fullName, passportNumber, idNumber, phone, email, password, UserRole.Operator)
        {
            _transactionInvoker = transactionInvoker;
        }

        public override void PerformRoleActions()
        {
            Console.WriteLine("1 - Просмотреть статистику  по движениям средств");
            Console.WriteLine("2 - Подтвердить заявку на зарплатный проект");
            Console.WriteLine("3 - Отменить перевод средств(можно только 1 раз)");
            Console.WriteLine("0 - Выйти");
        }

        public override void HandleAction(string choice)
        {
            LogDatabase logDb = new LogDatabase();

            switch (choice)
            {
                case "1":
                    TransactionDatabase transactionDb = new TransactionDatabase();
                    transactionDb.ShowTransactions();
                    break;
                case "2":
                    OperatorActions.SelectSalaryProjectApplication(_transactionInvoker);
                    logDb.AddLog(FullName, "Подтвердил зарплатный проект");
                    break;
                case "3":
                    if (hasUndoBeenPerformed)
                    {
                        Console.WriteLine("Отмена перевода уже была выполнена.");
                    }
                    else
                    {
                        OperatorActions.ShowFilteredCommandHistoryAndUndo(_transactionInvoker);
                        logDb.AddLog(FullName, "Отменил перевод средств");
                        hasUndoBeenPerformed = true;
                    }
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Некорректный ввод.");
                    break;
            }
        }
    }
}
