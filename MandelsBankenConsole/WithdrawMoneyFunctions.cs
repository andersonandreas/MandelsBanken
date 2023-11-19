using MandelsBankenConsole.Data;
using MandelsBankenConsole.Models;
using System.Runtime.InteropServices;

namespace MandelsBankenConsole
{
    internal class WithdrawMoneyFunctions
    {
        //View available accounts and balance.
        //Will take Magdas functions from DBHelper into here
        public static void WithdrawMoney(User loggedInUser, BankenContext context)
        {
            int selectedIndex = 0;
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Choose an account for withdrawal:");

                var availableAccounts = context.Users
                    .Where(u => u.Id == loggedInUser.Id)
                    .SelectMany(u => u.Accounts)
                    .Where(a => a.UserId == loggedInUser.Id)
                    .Select(a => new { a.AccountNumber, a.Balance, a.AccountName })
                    .ToArray();

                for (int i = 0; i < availableAccounts.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.Write("->");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine($"{availableAccounts[i].AccountNumber}, {availableAccounts[i].AccountName}: {availableAccounts[i].Balance}");
                }
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex - 1 + availableAccounts.Length) % availableAccounts.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex + 1) % availableAccounts.Length;
                        break;
                    case ConsoleKey.Enter:
                        int selectedAccountNumber = availableAccounts[selectedIndex].AccountNumber;
                        MakeMoneyWithdrawal(loggedInUser, context, selectedAccountNumber);
                        break;
                }
            }
        }
        //Use API converter to convert transactions from savings with foreign currency...
        //Show accounts with foreign currency, converted to SEK??
        //This method will ask for valid withdrawal amount + pin
        //Loop will check for valid input
        private static void MakeMoneyWithdrawal(User loggedInUser, BankenContext context, int selectedAccountNumber)
        {

            Console.Clear();
            Console.WriteLine("Please, enter withdrawal amount in SEK. If you want to exit withdrawal menu, press 1.");
            int userInput;
            decimal availableBalance = FetchBalance(context, loggedInUser.Id, selectedAccountNumber);

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
            if (!VerifyPin(context, loggedInUser.Id, selectedAccountNumber))
            {
                Console.WriteLine("Pin attempts exhausted. Returning to main manu.");
                Thread.Sleep(3000);
                return;
            }
            else
            {
                Console.Clear();
                UpdateAccountBalance(context, loggedInUser.Id, selectedAccountNumber, userInput);
                Console.WriteLine($"The withdrawal has been executed. New balance is: {(double)FetchBalance(context, loggedInUser.Id, selectedAccountNumber)} SEK.");
                Thread.Sleep(8000);
                return;
            }


        }




        //ADD TRANSACTION HISTORY BY USING METHOD FROM MAGDAS DBHELPER-METHOD

        //Method updates baalnce after withdrawal
        private static void UpdateAccountBalance(BankenContext context, int loggedInUser, int selectedAccountNumber, int withdrawalAmount)
        {
            var newAccountBalance = context.Accounts
            .Where(a => a.UserId == loggedInUser && a.AccountNumber == selectedAccountNumber)
            .FirstOrDefault();
            if (newAccountBalance != null )
            {
                newAccountBalance.Balance -= withdrawalAmount;
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Error.");
            }
        }
        //Method fetches current balance
        private static decimal FetchBalance(BankenContext context, int loggedInUser, int selectedAccountNumber)
        {
            var currentAccountBalance = context.Accounts
            .Where(u => u.UserId == loggedInUser && u.AccountNumber == selectedAccountNumber)
            .Select(u => u.Balance)
            .FirstOrDefault();
            return currentAccountBalance;
        }

        //Method for verifying pin. If bool returns true, transaction will be made.
        //If user fails three times, they will return to main menu.
        private static bool VerifyPin(BankenContext context, int loggedInUserId, int selectedAccountNumber)
        {
            int pinAttempts = 0;

            while (pinAttempts < 3)
            {
                Console.Clear();
                Console.WriteLine("Please, verify your withdrawal with pin.");
                string pin = Console.ReadLine();

                var loggedInUser = context.Users
                    .Where(u => u.Id == loggedInUserId)
                    .FirstOrDefault();

                if(loggedInUser != null && loggedInUser.Pin == pin)
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