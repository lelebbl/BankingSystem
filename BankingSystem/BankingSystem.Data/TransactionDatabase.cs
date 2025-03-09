using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace BankingSystem.BankingSystem.Data
{
    public class TransactionDatabase
    {
        private readonly string connectionString = "Data Source=transactions.db;Version=3;";

        public TransactionDatabase()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS transactions (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    timestamp TEXT,
                    user TEXT,
                    operationType TEXT,
                    amount DECIMAL(18,2),
                    accountNumber TEXT
                );";
                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddTransaction(string user, string operationType, decimal amount, string accountNumber)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO transactions (timestamp, user, operationType, amount, accountNumber) VALUES (@timestamp, @user, @operationType, @amount, @accountNumber);";
                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@user", user);
                    command.Parameters.AddWithValue("@operationType", operationType);
                    command.Parameters.AddWithValue("@amount", amount);
                    command.Parameters.AddWithValue("@accountNumber", accountNumber);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ShowTransactions()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM transactions;";
                using (var command = new SQLiteCommand(selectQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("\nИстория транзакций:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["timestamp"]} | {reader["user"]} | {reader["operationType"]} | {reader["amount"]} руб. | Счет: {reader["accountNumber"]}");
                    }
                }
            }
        }
    }
}
