using MandelsBankenConsole.AccountHandler;
using MandelsBankenConsole.Data;
using MandelsBankenConsole.InputValidator;
using MandelsBankenConsole.Models;
using MandelsBankenConsole.UserHandler;
using MandelsBankenConsole.Utilities;

namespace MandelsBankenConsole.MenuInteraction
{
    public class MenuFunctions
    {

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
            Console.WriteLine("Welcome to Mandelsbanken!");
            Console.WriteLine("Making banking smooth as almond milk");
            Console.WriteLine("Please log in");

            string userLogInInput = _validateUserInput.SocialNumber();

            const int maxAttempts = 3;
            int failedAttempts = 0;

            while (failedAttempts < maxAttempts)
            {
                string pin = _validateUserInput.Pin();

                if (userLogInInput.ToLower() == "admin")
                {
                    if (pin != "1234")
                    {
                        failedAttempts++;
                        ConsoleHelper.PrintColorRed($"Invalid password. Attempts remaining: {maxAttempts - failedAttempts}");
                    }
                    else
                    {
                        _adminFunctions.DoAdminTasks();
                        return;
                    }
                }
                else
                {
                    loggedInUser = DbHelper.GetUserByLogInInput(_bankenContext, userLogInInput);

                    if (loggedInUser == null || loggedInUser.Pin != pin)
                    {
                        failedAttempts++;
                        ConsoleHelper.PrintColorRed($"Invalid username or password. Attempts remaining: {maxAttempts - failedAttempts}");
                    }
                    else
                    {

                        Console.Clear();
                        Console.WriteLine($"Welcome {loggedInUser.CustomerName}!");

                        Thread.Sleep(750);
                        List<string> userMenuOptions = new List<string>
                        {
                            "See your accounts and balance",
                            "Transfer between accounts",
                            "Withdraw money",
                            "Deposit money",
                            "Open a new account",
                            "Log out"

                        };


                        while (true)
                        {
                            int choice = ShowMenu(userMenuOptions);
                            ExecuteMenuOption(choice);
                        }
                    }
                }
            }

            ConsoleHelper.PrintColorRed("Maximum number of login attempts has been reached. The login is locked for 3 minutes.");
            Thread.Sleep(180000);
            ConsoleHelper.PrintColorGreen("\nReturning to main menu....");
            Thread.Sleep(2000);
            Console.Clear();
            LogIn();
        }



        public int ShowMenu(List<string> menuOptions, string title = "Menu", string titleExtra = "alternatives")
        {

            int selectedIndex = 0;

            // if the list is long it will be divided into more than one sides
            // in this case we need arrows right/left to navigate between subsides 

            int maxAlternativesPerSide = 10;

            List<string> partialMenuOptions;
            int partsOfMenu = (int)(Math.Ceiling((double)menuOptions.Count / maxAlternativesPerSide));

            int amountAlternativesOnThisSide = default;
            int part = 1;

            while (true)
            {

                amountAlternativesOnThisSide = partsOfMenu > part ? maxAlternativesPerSide : ((int)(double)menuOptions.Count - 1) % maxAlternativesPerSide + 1;

                partialMenuOptions = menuOptions.
                    GetRange(
                        (part - 1) * maxAlternativesPerSide,
                        amountAlternativesOnThisSide);

                Console.Clear();
                Console.WriteLine(title);

                for (int i = 0; i < amountAlternativesOnThisSide; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.Write("-> ");
                    }
                    else
                    {
                        Console.Write("   ");
                    }

                    Console.WriteLine(partialMenuOptions[i]);
                }
                if (partsOfMenu != 1)
                {
                    ConsoleHelper.PrintColorGreen($"Page {part} of {partsOfMenu}. Press right or left to see more {titleExtra}");
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex - 1 + partialMenuOptions.Count) % partialMenuOptions.Count;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex + 1) % partialMenuOptions.Count;
                        break;
                    case ConsoleKey.LeftArrow:
                        part = (part + partsOfMenu) % partsOfMenu + 1;
                        selectedIndex = 0;
                        break;
                    case ConsoleKey.RightArrow:
                        part = part % partsOfMenu + 1;
                        selectedIndex = 0;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        return selectedIndex + (part - 1) * maxAlternativesPerSide;

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