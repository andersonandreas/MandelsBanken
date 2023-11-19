using MandelsBankenConsole.CurrencyConverter;
using MandelsBankenConsole.Data;
using MandelsBankenConsole.Models;
using MandelsBankenConsole.Utilities;

namespace MandelsBankenConsole.AccountHandler
{
    public class WithdrawMoneyFunctions
    {
        //View available accounts and balance.
        //Will take Magdas functions from DBHelper into here


        private readonly BankenContext _bankenContext;
        private readonly ExchangeCurrency _conversion;

        private readonly MenuFunctions _menuFunctions = new MenuFunctions();


        //_conversion.ConvertCurrency();

        public WithdrawMoneyFunctions(BankenContext bankenContext, ExchangeCurrency exchangeCurrency)
        {
            _bankenContext = bankenContext;
            _conversion = exchangeCurrency;

        }

        public void WithdrawMoney(User loggedInUser)
        {
            int selectedIndex = 0;
            List<Account> userAccounts = DbHelper.GetAllAccounts(_bankenContext, loggedInUser);
            List<string> userAccountsDesc = DbHelper.GetAccountInformation(userAccounts);
            // if user only has 1 account, this is the default withdrawal account = 0 in the list
            // if more than 1 account, choose from the list
            if (userAccounts.Count > 1)
            {
                selectedIndex = _menuFunctions.ShowMenu(userAccountsDesc.ToArray(), "Choose an account for withdrawal:");
            }
            int selectedAccountNumber = userAccounts[selectedIndex].AccountNumber;
            MakeMoneyWithdrawal(loggedInUser, selectedAccountNumber);

        }
        //Use API converter to convert transactions from savings with foreign currency...
        //Show accounts with foreign currency, converted to SEK??
        //This method will ask for valid withdrawal amount + pin
        //Loop will check for valid input
        private void MakeMoneyWithdrawal(User loggedInUser, int selectedAccountNumber)
        {

            Console.Clear();
            Console.WriteLine("Please, enter withdrawal amount in SEK. If you want to exit withdrawal menu, press 1.");
            int userInput;
            decimal availableBalance = FetchBalance(loggedInUser.Id, selectedAccountNumber);

            while (true) //fixA?
            {
                string stringUserInput = Console.ReadLine();
                if (int.TryParse(stringUserInput, out userInput))
                {
                    if (userInput > 0)
                    {
                        if (userInput == 1)
                        {
                            Console.WriteLine("Returning to menu.");
                            Thread.Sleep(3000);
                            return;
                        }
                        else if (userInput > availableBalance)
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid amount or insufficient balance. Try again.");
                        }
                        else
                        {
                            Console.WriteLine("Loading... Did you know that almonds are related to peaches?");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please, enter a positive number.");
                        Thread.Sleep(3000);
                    }

                }
                else
                {
                    Console.WriteLine("Invalid input. Please, enter a valid number.");
                }

            }
            //Going into this selection, to see if transaction will or will not be made.
            if (!VerifyPin(loggedInUser.Id, selectedAccountNumber))
            {
                Console.WriteLine("Pin attempts exhausted. Returning to main manu.");
                Thread.Sleep(3000);
                return;
            }
            else
            {
                Console.Clear();
                UpdateAccountBalance(loggedInUser.Id, selectedAccountNumber, userInput);
                Console.WriteLine($"The withdrawal has been executed. New balance is: {(double)FetchBalance(loggedInUser.Id, selectedAccountNumber)} SEK.");
                Thread.Sleep(8000);
                return; //Blir fel här, måste tillbaka till main...
            }
            return;

        }




        //ADD TRANSACTION HISTORY BY USING METHOD FROM MAGDAS DBHELPER-METHOD

        //Method updates baalnce after withdrawal
        private void UpdateAccountBalance(int loggedInUser, int selectedAccountNumber, int withdrawalAmount)
        {
            var newAccountBalance = _bankenContext.Accounts
            .Where(a => /*a.UserId == loggedInUser &&*/ a.AccountNumber == selectedAccountNumber)
            .FirstOrDefault();
            if (newAccountBalance != null)
            {
                newAccountBalance.Balance -= withdrawalAmount;
                _bankenContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("Error.");
            }
        }
        //Method fetches current balance
        private decimal FetchBalance(int loggedInUser, int selectedAccountNumber)
        {
            var currentAccountBalance = _bankenContext.Accounts
            .Where(u => /*u.UserId == loggedInUser &&*/ u.AccountNumber == selectedAccountNumber)
            .Select(u => u.Balance)
            .FirstOrDefault();
            return currentAccountBalance;
        }

        //Method for verifying pin. If bool returns true, transaction will be made.
        //If user fails three times, they will return to main menu.
        private bool VerifyPin(int loggedInUserId, int selectedAccountNumber)
        {
            int pinAttempts = 0;

            while (pinAttempts < 3)
            {
                Console.Clear();
                Console.WriteLine("Please, verify your withdrawal with pin.");
                string pin = Console.ReadLine();

                var loggedInUser = _bankenContext.Users
                    .Where(u => u.Id == loggedInUserId)
                    .FirstOrDefault();

                if (loggedInUser != null && loggedInUser.Pin == pin)
                {
                    return true;
                }
                else
                {
                    pinAttempts++;
                }
            }
            return false;
        }
    }
}
