using MandelsBankenConsole.CurrencyConverter;
using MandelsBankenConsole.Data;
using MandelsBankenConsole.InputValidator;
using MandelsBankenConsole.MenuInteraction;
using MandelsBankenConsole.Models;
using MandelsBankenConsole.Utilities;
namespace MandelsBankenConsole.AccountHandler
{
    public class WithdrawMoneyFunctions
    {


        private readonly BankenContext _bankenContext;
        private readonly ExchangeCurrency _conversion;
        private readonly IValidateUserInput _validateUserInput;
        private readonly MenuFunctions _menuFunctions = new MenuFunctions();

        public WithdrawMoneyFunctions(BankenContext bankenContext, ExchangeCurrency exchangeCurrency, IValidateUserInput validateUserInput)
        {
            _bankenContext = bankenContext;
            _conversion = exchangeCurrency;
            _validateUserInput = validateUserInput;
        }



        public void WithdrawMoney(User loggedInUser)
        {
            int selectedIndex = 0;
            List<Account> userAccounts = DbHelper.GetAllAccounts(_bankenContext, loggedInUser);
            List<string> userAccountsDescription = DbHelper.GetAccountInformation(userAccounts);
            // if user only has 1 account, this is the default withdrawal account = 0 in the list
            // if more than 1 account, choose from the list
            if (userAccounts.Count > 1)
            {
                selectedIndex = _menuFunctions.ShowMenu(userAccountsDescription, "Choose an account for withdrawal:");
            }
            Account selectedAccount = userAccounts[selectedIndex];
            MakeMoneyWithdrawal(loggedInUser, selectedAccount);
        }

        //This method will ask for valid withdrawal amount + pin
        //Loop will check for valid input
        private void MakeMoneyWithdrawal(User loggedInUser, Account account)
        {

            Console.Clear();
            Console.WriteLine("Please, enter withdrawal amount in SEK. If you want to exit withdrawal menu, press 1.");
            int userInput;
            decimal availableBalance = account.Balance;
            decimal amount;
            string accountCurrencyCode = account.Currency.CurrencyCode;
            if (availableBalance == 0)
            {
                Console.Clear();
                ConsoleHelper.PrintColorRed("You do not have any money in the account. You will return to main menu.");
                return;
            }
            while (true)
            {
                string stringUserInput = Console.ReadLine();
                if (int.TryParse(stringUserInput, out userInput))
                {
                    if (userInput > 0)
                    {
                        if (userInput == 1)
                        {
                            Console.WriteLine("Press enter again to return to main menu.");
                            return;
                        }

                        else
                        {
                            Console.WriteLine("Loading... Did you know that almonds are related to peaches?");
                            Thread.Sleep(2000);

                            var conversionResult = Task.Run(() => _conversion.ConvertCurrency("SEK", accountCurrencyCode, userInput)).Result;

                            var (resultIndecimal, infoDescription) = conversionResult;
                            amount = resultIndecimal;
                            break;
                        }
                    }
                    else
                    {
                        ConsoleHelper.PrintColorRed("Invalid input. Please, enter a positive number.");
                    }
                }
                else
                {
                    ConsoleHelper.PrintColorRed("Invalid input. Please, enter a valid number.");
                }
            }
            //Going into this selection, to see if transaction will or will not be made.
            if (!VerifyPin(loggedInUser.Id))
            {
                ConsoleHelper.PrintColorRed("Pin attempts exhausted. Press enter to return to main menu.");
                return;
            }
            else
            {
                Console.Clear();

                if (DbHelper.MakeTransaction(_bankenContext, account, -amount, "Withdrawal"))
                {
                    ConsoleHelper.PrintColorGreen($"The withdrawal has been executed. New balance is: {account.Balance:# ##0.##} {accountCurrencyCode}. Press enter to return to main menu.");
                    Console.ReadKey();
                }
            }
        }

        //Method for verifying pin. If bool returns true, transaction will be made.
        //If user fails three times, they will return to main menu.
        private bool VerifyPin(int loggedInUserId)
        {
            string usersPin = _bankenContext.Users
            .Where(u => u.Id == loggedInUserId)
            .Select(u => u.Pin)
            .FirstOrDefault();

            Console.Clear();
            Console.WriteLine("Please, verify your withdrawal with pin.");

            const int maxAttemps = 3;
            int pinAttempts = 0;
            while (pinAttempts < maxAttemps)
            {

                string pin = _validateUserInput.Pin();

                if (usersPin == pin)
                {
                    return true;
                }
                else
                {
                    pinAttempts++;
                    ConsoleHelper.PrintColorRed($"\nWrong pin. Attempts remaining: {maxAttemps - pinAttempts}.");
                }
            }
            return false;
        }
    }
}











