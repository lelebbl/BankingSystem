using BankingSystem.BankingSystem.Core.Entities.Users;
using BankingSystem.BankingSystem.Core.Services;
using BankingSystem.BankingSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.UI
{
    public class OperatorUi : IUserUi
    {
        private Operator _operator;
        private LogDatabase _logDb;
        private bool _hasUndoBeenPerformed = false;

        public OperatorUi(Operator operatorUser)
        {
            _operator = operatorUser;
            _logDb = new LogDatabase();
        }

        public void PerformRoleActions()
        {
            Console.WriteLine("1 - Просмотреть статистику по движениям средств");
            Console.WriteLine("2 - Подтвердить заявку на зарплатный проект");
            Console.WriteLine("3 - Отменить перевод средств (можно только 1 раз)");
            Console.WriteLine("0 - Выйти");
        }

        public void HandleAction(string choice)
        {
            switch (choice)
            {
                case "1":
                    TransactionDatabase transactionDb = new TransactionDatabase();
                    transactionDb.ShowTransactions();
                    break;
                case "2":
                    OperatorActions.SelectSalaryProjectApplication(_operator.TransactionInvoker);
                    _logDb.AddLog(_operator.FullName, "Подтвердил зарплатный проект");
                    break;
                case "3":
                    if (_hasUndoBeenPerformed)
                    {
                        Console.WriteLine("Отмена перевода уже была выполнена.");
                    }
                    else
                    {
                        OperatorActions.ShowFilteredCommandHistoryAndUndo(_operator.TransactionInvoker);
                        _logDb.AddLog(_operator.FullName, "Отменил перевод средств");
                        _hasUndoBeenPerformed = true;
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
