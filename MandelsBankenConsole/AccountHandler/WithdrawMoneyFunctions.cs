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
            Account selectedAccount = userAccounts[selectedIndex];
            MakeMoneyWithdrawal(loggedInUser, selectedAccount);

        }
        //Use API converter to convert transactions from savings with foreign currency...
        //Show accounts with foreign currency, converted to SEK??
        //This method will ask for valid withdrawal amount + pin
        //Loop will check for valid input
        private async void MakeMoneyWithdrawal(User loggedInUser, Account account)
        {
            //if (selectedAccountNumber.Currency.CurrencyCode  == "SEK") { }

            Console.Clear();
            Console.WriteLine("Please, enter withdrawal amount in SEK. If you want to exit withdrawal menu, press 1.");
            int userInput;
            decimal availableBalance = account.Balance;
            decimal amount;
            string accountCurrencyCode = account.Currency.CurrencyCode;

            if (availableBalance == 0)
            {
                Console.Clear();
                Console.WriteLine("You do not have any money in the account. You will return to main menu.");
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
                            Console.WriteLine("Returning to menu.");
                            return;
                        }
                        //else if (userInput > availableBalance)
                        //{
                        //    Console.Clear();
                        //    Console.WriteLine("Invalid amount or insufficient balance. Try again.");
                        //}
                        else
                        {
                            var (resultIndecimal, infoDescription) = await _conversion.ConvertCurrency(accountCurrencyCode, "SEK", userInput);
                            amount = resultIndecimal;
                            Console.WriteLine("Loading... Did you know that almonds are related to peaches?");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please, enter a positive number.");
                    }

                }
                else
                {
                    Console.WriteLine("Invalid input. Please, enter a valid number.");
                }

            }
            //Going into this selection, to see if transaction will or will not be made.
            if (!VerifyPin(loggedInUser.Id))
            {
                Console.WriteLine("Pin attempts exhausted. Returning to main manu.");
                Thread.Sleep(3000);
                return;
            }
            else
            {
                Console.Clear();
                if (DbHelper.MakeTransaction(_bankenContext, account, -amount, "withdrawal"))
                {
                    //UpdateAccountBalance(loggedInUser.Id, selectedAccountNumber, userInput);
                    await Console.Out.WriteLineAsync($"The withdrawal has been executed. New balance is: {account.Balance} {accountCurrencyCode}");
                    Thread.Sleep(3000);
                    Console.ReadKey();
                }


            }


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
        private bool VerifyPin(int loggedInUserId)
        {
            string usersPin = _bankenContext.Users
            .Where(u => u.Id == loggedInUserId)
            .Select(u => u.Pin)
            .FirstOrDefault();
            int pinAttempts = 0;

            while (pinAttempts < 3)
            {
                Console.Clear();
                Console.WriteLine("Please, verify your withdrawal with pin.");
                string pin = Console.ReadLine();


                Console.WriteLine();
                if (usersPin == pin)
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
