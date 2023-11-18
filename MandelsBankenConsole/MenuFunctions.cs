using MandelsBankenConsole.Data;
using MandelsBankenConsole.Models;
using MandelsBankenConsole.Utilities;

namespace MandelsBankenConsole
{
    internal static class MenuFunctions
    {
        private static User loggedInUser;
        public static void LogIn()
        {
            Console.WriteLine("Welcome to Mandelsbank!");
            Thread.Sleep(500);

            Console.WriteLine("Making banking smooth as almond milk");
            Thread.Sleep(750);

            Console.WriteLine("Please log in");

            Console.Write("Enter username (social security number YYYYMMDD-XXXX):");
            string userLogInInput = Console.ReadLine();

            Console.Write("Enter pin code:");
            string pin = Console.ReadLine();

            int choice = 0;
            if (userLogInInput == "admin")
            {
                if (pin != "1234")
                {
                    Console.WriteLine("Invalid password!");
                    return;
                }
                AdminFunctions.DoAdminTasks();
                return;
            }
            else
            {
                using (BankenContext context = new BankenContext())
                {
                    loggedInUser = DbHelper.GetUserByLogInInput(context, userLogInInput);

                    if (loggedInUser == null || loggedInUser.Pin != pin)
                    {
                        Console.WriteLine("Invalid username or password!");
                        return;
                    }
                    Console.Clear();
                    Console.WriteLine($"Welcome {loggedInUser.CustomerName}!");
                    Thread.Sleep(750);
                    string[] userMenuOptions = {
                        "See your accounts and balance",
                        "Transfer between accounts",
                        "Withdraw money",
                        "Deposit money",
                        "Open a new account",
                        "Log out"
                        };
                    while (true)
                    { 
                        choice = ShowMenu(userMenuOptions);
                        ExecuteMenuOption(context, choice);
                    }
                }
            }
        }
       
        public static int ShowMenu(string[] menuOptions, string title="Menu")
        {
            // Magda.ideer:
            // -- vi ska ha en inramning
            // högst upp ska det alltid stå Mandelsbanken + motto
            // längst ner ska det alltid stå "press Q to log out" or sth


            int selectedIndex = 0;
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine(title);

                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.Write("-> ");
                    }
                    else
                    {
                        Console.Write("   ");
                    }

                    Console.WriteLine(menuOptions[i]);
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex - 1 + menuOptions.Length) % menuOptions.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex + 1) % menuOptions.Length;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        return selectedIndex;

                }
            }
        }

        private static bool ExecuteMenuOption(BankenContext context,int optionIndex)
        {
            switch (optionIndex)
            {
                case 0:
                    Console.WriteLine("Does first option...");
                    Banking.ShowAccounts(context,loggedInUser);
                    Console.ReadLine();
                    break;
                case 1:
                    Console.WriteLine("Does second option...");
                    Banking.BankTransfer(context,loggedInUser);
                    Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("Does third option...");
                    Console.ReadLine();
                    break;
                case 3:
                    Console.WriteLine("Does fourth option...");
                    Console.ReadLine();
                    break;
                case 4:
                    Console.WriteLine("Does fifth option...");
                    Console.ReadLine();
                    break;
                case 5:
                    Console.WriteLine("Logging out...");
                    loggedInUser = null;
                    Thread.Sleep(500);
                    Console.Clear();
                    LogIn();
                    return false;
            }
            return true;
        }
    }
}