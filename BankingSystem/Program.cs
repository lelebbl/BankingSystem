using BankingSystem.BankingSystem.Core.Commands;
using BankingSystem.BankingSystem.Core.Entities;
using BankingSystem.BankingSystem.Core.Entities.Banks;
using BankingSystem.BankingSystem.Core.Entities.Users;
using BankingSystem.BankingSystem.Core.Enums;
using BankingSystem.BankingSystem.Core.Services;
using BankingSystem.BankingSystem.Data;

class Program
{
    static CommandInvoker transactionInvoker = new CommandInvoker();
    static AuthService authService = new AuthService(transactionInvoker);
    static BankService bankService = new BankService();
    static LogDatabase logDb = new LogDatabase();
    public static Bank selectedBank;
    public static User currentUser;


    static void Main()
    {
        logDb.AddLog("Система", "Запуск программы");
        while (true)
        {
            SelectBank();
        }
    }

    static void SelectBank()

    {
        Console.WriteLine("\nВыберите банк:");
        for (int i = 0; i < bankService.Banks.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {bankService.Banks[i].Name}");
        }

        Console.Write("Введите номер банка: ");
        if (int.TryParse(Console.ReadLine(), out int bankIndex) &&
            bankIndex > 0 && bankIndex <= bankService.Banks.Count)
        {
            selectedBank = bankService.Banks[bankIndex - 1];
            logDb.AddLog("Система", $"Выбран банк: {selectedBank.Name}");
            ShowMainMenu();
        }
        else
        {
            Console.WriteLine("Некорректный ввод, попробуйте снова.");
        }
    }


    static void ShowMainMenu()
    {
        Console.WriteLine($"\nВы работаете с {selectedBank.Name}");
        Console.WriteLine("\nДобро пожаловать в банковскую систему!");
        Console.WriteLine("1 - Войти");
        Console.WriteLine("2 - Зарегистрироваться");
        Console.WriteLine("3 - Сменить банк");
        Console.WriteLine("4 - Выйти");

        switch (Console.ReadLine())
        {
            case "1": Login(); break;
            case "2": Register(); break;
            case "3": SelectBank(); break; 
            case "4":
                logDb.AddLog("Система", "Выход из программы");
                Environment.Exit(0);
                break;
            default: Console.WriteLine("Некорректный ввод."); break;
        }
    }

    static void Login()
    {
        Console.Write("Введите email: ");
        string email = Console.ReadLine();
        Console.Write("Введите пароль: ");
        string password = Console.ReadLine();
        var user = authService.Login(email, password);

        if (user != null)
        {
            if (!user.IsApproved)
            {
                Console.WriteLine("Ваш аккаунт еще не одобрен менеджером.");
                return;
            }
            logDb.AddLog(user.FullName, $"Вход в систему (роль: {user.Role})");
            Console.WriteLine($"Вы вошли в систему как {user.Role}");
            Console.WriteLine($"Добро пожаловать, {user.FullName}");
            ShowMenu(user);
        }
        else
        {
            Console.WriteLine("Пользователь не найден.");
        }
    }

    static void Register()
    {
        Console.WriteLine("Выберите роль:");
        Console.WriteLine("1 - Клиент");
        Console.WriteLine("2 - Оператор");
        Console.WriteLine("3 - Менеджер");
        Console.WriteLine("4 - Специалист");
        Console.WriteLine("5 - Администратор");

        if (int.TryParse(Console.ReadLine(), out int roleChoice) && roleChoice >= 1 && roleChoice <= 5)
        {
            UserRole selectedRole = (UserRole)(roleChoice - 1);

            Console.Write("Введите ФИО: ");
            string fullName = Console.ReadLine();
            Console.Write("Введите серию и номер паспорта: ");
            string passport = Console.ReadLine();
            Console.Write("Введите идентификационный номер: ");
            string id = Console.ReadLine();
            Console.Write("Введите телефон: ");
            string phone = Console.ReadLine();
            Console.Write("Введите email: ");
            string email = Console.ReadLine();
            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();

            User newUser = authService.Register(fullName, passport, id, phone, email, password, selectedRole);
            logDb.AddLog(fullName, $"Зарегистрирован новый пользователь (роль: {selectedRole})");

            if (selectedRole == UserRole.Client)
            {
                Console.WriteLine("Ожидайте одобрения менеджера.");
            }
            else
            {
                Console.WriteLine($"Добро пожаловать, {newUser.FullName}!");
                ShowMenu(newUser);
            }
        }
        else
        {
            Console.WriteLine("Некорректный ввод, попробуйте снова.");
        }
    }


    static void ShowMenu(User user)
    {
        while (true)
        {
            Console.WriteLine("\nДоступные действия:");
            user.PerformRoleActions();
            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            if (choice == "0")
            {
                Console.WriteLine("Возврат в главное меню...");
                ShowMainMenu();
                break;
            }

            user.HandleAction(choice);
        }
    }
}