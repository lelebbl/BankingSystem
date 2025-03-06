using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.BankingSystem.Core.Services
{
    public class LogManager
    {
        private static LogManager _instance;
        private readonly string _logFilePath = "logs.txt";
        private readonly string _encryptionKey = "encryption_key_123"; 

        private LogManager() { }

        public static LogManager Instance => _instance ??= new LogManager();

        public void Log(string message)
        {
            string encryptedMessage = Encrypt(message);
            File.AppendAllText(_logFilePath, $"{DateTime.Now}: {encryptedMessage}{Environment.NewLine}");
        }

        public void ViewLogs()
        {
            if (File.Exists(_logFilePath))
            {
                string[] encryptedLogs = File.ReadAllLines(_logFilePath);
                foreach (var encryptedLog in encryptedLogs)
                {
                    string decryptedMessage = Decrypt(encryptedLog);
                    Console.WriteLine(decryptedMessage);
                }
            }
            else
            {
                Console.WriteLine("Логи отсутствуют.");
            }
        }

        private string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            var key = Encoding.UTF8.GetBytes(_encryptionKey);
            using var encryptor = aes.CreateEncryptor(key, aes.IV);

            using var ms = new MemoryStream();
            ms.Write(aes.IV, 0, aes.IV.Length);
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using var sw = new StreamWriter(cs);
            sw.Write(plainText);

            return Convert.ToBase64String(ms.ToArray());
        }

        private string Decrypt(string cipherText)
        {
            var fullCipher = Convert.FromBase64String(cipherText);
            using var aes = Aes.Create();
            var key = Encoding.UTF8.GetBytes(_encryptionKey);
            var iv = new byte[aes.IV.Length];
            Array.Copy(fullCipher, iv, iv.Length);
            using var decryptor = aes.CreateDecryptor(key, iv);

            using var ms = new MemoryStream(fullCipher, iv.Length, fullCipher.Length - iv.Length);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
    }
}
