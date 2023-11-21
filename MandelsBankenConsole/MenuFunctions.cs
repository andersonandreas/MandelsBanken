using MandelsBankenConsole.AccountHandler;
using MandelsBankenConsole.Data;
using MandelsBankenConsole.InputValidator;
using MandelsBankenConsole.Models;
using MandelsBankenConsole.UserHandler;
using MandelsBankenConsole.Utilities;

namespace MandelsBankenConsole
{
    public class MenuFunctions
    {

        //changed the class to public it easier otherwise we need to change all the classes to internal in the whole project
        // removed the static keyword on the methods. its not needed and we cant use the field of loggedInUser and lpass the other method to each others
        // we need to put all classes that we build here as Depenciys injections

        private readonly BankenContext _bankenContext;
        private readonly AccountManager _accountManager;
        private readonly DepositMoneyFunctions _depositMoneyFunctions;
        private readonly AdminFunctions _adminFunctions;
        private readonly BankTransfer _bankTransfer;
        private readonly WithdrawMoneyFunctions _withdrawMoneyFunctions;
        private readonly ShowAccount _showAccount;
        private readonly IValidateUserInput _validateUserInput;


        private static User loggedInUser;
        public MenuFunctions() { }

        public MenuFunctions(BankenContext bankenContext, AccountManager accountManager, DepositMoneyFunctions depositMoneyFunctions,
            AdminFunctions adminFunctions, BankTransfer banking, WithdrawMoneyFunctions withdrawMoneyFunctions, ShowAccount showAccount,
            IValidateUserInput validateUserInput = null)
        {
            _bankenContext = bankenContext;
            _accountManager = accountManager;
            _depositMoneyFunctions = depositMoneyFunctions;
            _adminFunctions = adminFunctions;
            _bankTransfer = banking;
            _withdrawMoneyFunctions = withdrawMoneyFunctions;
            _showAccount = showAccount;
            _validateUserInput = validateUserInput;
        }

        public void LogIn()
        {

            Console.WriteLine("Welcome to Mandelsbank!");
            //Thread.Sleep(500);

            Console.WriteLine("Making banking smooth as almond milk");
            //Thread.Sleep(750);

            Console.WriteLine("Please log in");

            //Console.WriteLine("Enter socialnumber: ");
            string userLogInInput = _validateUserInput.SocialNumber();
            string pin = _validateUserInput.Pin();

            int choice = 0;
            if (userLogInInput.ToLower() == "admin")
            {
                if (pin != "1234")
                {
                    ConsoleHelper.PrintColorRed("invalid password");
                    return;
                }

                _adminFunctions.DoAdminTasks();
                return;
            }
            else
            {
                // removed the context here we take the context from the injection so we now the we working with just one connetcion object in the whole project

                loggedInUser = DbHelper.GetUserByLogInInput(_bankenContext, userLogInInput);

                if (loggedInUser == null || loggedInUser.Pin != pin)
                {
                    ConsoleHelper.PrintColorRed("Invalid username or password!");
                    return;
                }
                Console.Clear();
                Console.WriteLine($"Welcome {loggedInUser.CustomerName}!");

                Thread.Sleep(750);
                List<string> userMenuOptions = new List<string> {
                  
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
                    ExecuteMenuOption(choice);
                }

            }
        }
        public int ShowMenu(List<string> menuOptions, string title = "Menu")
        {

            int selectedIndex = 0;
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine(title);

                for (int i = 0; i < menuOptions.Count; i++)
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
                        selectedIndex = (selectedIndex - 1 + menuOptions.Count) % menuOptions.Count;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex + 1) % menuOptions.Count;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        return selectedIndex;

                }
            }
        }


        private bool ExecuteMenuOption(int optionIndex)
        {
            switch (optionIndex)
            {
                case 0:
                    _showAccount.ShowAccounts(loggedInUser);
                    Console.ReadLine();
                    break;
                case 1:
                    _bankTransfer.MakeTransfer(loggedInUser);
                    Console.ReadLine();
                    break;
                case 2:
                    _withdrawMoneyFunctions.WithdrawMoney(loggedInUser);
                    Console.ReadLine();
                    break;
                case 3:
                    _depositMoneyFunctions.DepositMoney(loggedInUser);
                    Console.ReadLine();
                    break;
                case 4:
                    _accountManager.CreateAccount(loggedInUser);
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

        public static void ResetLoggedInUser()
        {
            loggedInUser = null;
        }
    }
}