using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace BankingSystem.BankingSystem.Data
{
    public class LogDatabase
    {
        private readonly string connectionString = "Data Source=logs.db;Version=3;";

        public LogDatabase()
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS logs (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    timestamp TEXT,
                    user TEXT,
                    action TEXT
                );";
                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddLog(string user, string action)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO logs (timestamp, user, action) VALUES (@timestamp, @user, @action);";
                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@user", user);
                    command.Parameters.AddWithValue("@action", action);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ShowLogs()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM logs;";
                using (var command = new SQLiteCommand(selectQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine("\nИстория логов:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["timestamp"]} | {reader["user"]} | {reader["action"]}");
                    }
                }
            }
        }
    }

}
